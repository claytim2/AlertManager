using DataAccess.Context;
using Infra;
using Interface;
using Localization.Model;
using Model.AbsModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataServices
{
    public class BaseService<T> where T : class, IBaseModel
    {

        /// <summary>
        /// Lista todos os registros da tabela
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> GetAll(int siteId = 0, bool filterSite = true)
        {
            if (filterSite && siteId == 0)
                siteId = GlobalFunctions.SiteId;

            using (var context = new DatabaseContext())
            {
                var query = context.Set<T>().AsQueryable()
                    .Where(p => !filterSite || p.SiteId == siteId);

                if (Includes.Any())
                    foreach (string include in Includes)
                    {
                        query = query.Include(include);
                    }

                return query.AsNoTracking().ToList();
            }
        }

        public int Count(int siteId = 0, bool filterSite = true, Expression<Func<T, bool>> filter = null)
        {
            if (filterSite && siteId == 0)
                siteId = GlobalFunctions.SiteId;

            using (var context = new DatabaseContext())
            {
                if (filter != null)
                    return context.Set<T>()
                        .Where(p => !filterSite || p.SiteId == siteId)
                        .Where(filter)
                        .Count();
                else
                    return context.Set<T>()
                        .Where(p => !filterSite || p.SiteId == siteId)
                        .Count();

            }
        }


        /// <summary>
        /// Retorna uma entidade filtrado pela expressão passada
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public IEnumerable<T> GetByFilter(
            Expression<Func<T, bool>> filter,
            int siteId = 0,
            bool filterSite = true,
            Func<T, object> orderingFunction = null,
            bool orderingAsc = true,
            int skip = 0,
            int pageLength = -1)
        {
            if (filterSite && siteId == 0)
                siteId = GlobalFunctions.SiteId;

            using (var context = new DatabaseContext())
            {
                IQueryable<T> query = context.Set<T>()
                        .Where(p => !filterSite || p.SiteId == siteId)
                        .Where(filter)
                        .AsQueryable();

                if (Includes.Any())
                    foreach (string include in Includes)
                    {
                        query = query.Include(include);
                    }

                if (orderingAsc)
                    query = query.OrderBy(orderingFunction ?? (p => "")).AsQueryable();
                else
                    query = query.OrderByDescending(orderingFunction ?? (p => "")).AsQueryable();

                query = query.Skip(skip);

                if (pageLength > -1)
                    query = query.Take(pageLength);

                return query.AsNoTracking().ToList();
            }
        }

        public T GetById(int Id)
        {
            using (var context = new DatabaseContext())
            {
                if (!Includes.Any())
                {
                    return context.Set<T>().Find(Id);
                }
                else
                {
                    var query = context.Set<T>().Where(p => p.Id == Id).AsQueryable();
                    foreach (string include in Includes)
                    {
                        query = query.Include(include);
                    }
                    return query.AsNoTracking().ToList().FirstOrDefault();
                }
            }

        }

        public ResultProcessing Save(T entity, bool filterSite = true, bool saveLog = true, bool forceCreate = false)
        {
            var result = new ResultProcessing();
            var differences = new List<string>();
            try
            {
                if (filterSite && entity.SiteId == 0)
                    entity.SiteId = GlobalFunctions.SiteId;
                var newId = entity.Id;
                var oldEntityJson = "";
                using (var context = new DatabaseContext())
                {
                    //Registro novo
                    if (newId == 0 || forceCreate)
                    {
                        entity.UserCreate = UserAuthService.RetornaUsuarioLogado().UserName;
                        entity.DateCreate = DateTime.Now;
                    }
                    else //Atualização de registro
                    {
                        //Mantendo os valores antigos
                        var oldEntity = GetById(newId);
                        oldEntityJson = oldEntity.RemoveProxies().ToJSON();
                        entity.UserCreate = oldEntity.UserCreate;
                        entity.DateCreate = oldEntity.DateCreate;
                        entity.UserUpdate = oldEntity.UserUpdate;
                        entity.DateUpdate = oldEntity.DateUpdate;

                        differences = GlobalFunctions.GenerateAuditLogMessages<T>(oldEntity.RemoveProxies(), entity);

                        if (saveLog)
                        {
                            entity.UserUpdate = UserAuthService.RetornaUsuarioLogado().UserName;
                            entity.DateUpdate = DateTime.Now;
                        }



                    }
                    context.Entry(entity).State = (entity.Id == 0 || forceCreate ? EntityState.Added : EntityState.Modified);
                    context.SaveChanges();
                }
                result.Success = true;
                result.AdditionalData = newId == 0 ? entity.Id : 0;

                //Log
                if (saveLog &&
                    !typeof(T).Name.ToLower().Equals("log") &&
                    !typeof(T).Name.ToLower().Equals("agentresult") &&
                    !typeof(T).Name.ToLower().Equals("humidity") &&
                    !typeof(T).Name.ToLower().Equals("temperature") &&
                    (!typeof(T).Name.ToLower().Equals("notification") || (typeof(T).Name.ToLower().Equals("notification") && newId != 0)) &&
                    !typeof(T).Name.ToLower().Equals("area") &&
                    !typeof(T).Name.ToLower().Equals("areamaster") &&
                    !typeof(T).Name.ToLower().Equals("employeeregistration") &&
                    !typeof(T).Name.ToLower().Equals("equipment") &&
                    !typeof(T).Name.ToLower().Equals("line") &&
                    !typeof(T).Name.ToLower().Equals("sitejabil") &&
                    (!typeof(T).Name.ToLower().Equals("configuration") || (typeof(T).Name.ToLower().Equals("configuration") && newId != 0))
                )
                {
                    LogService.Save(newId == 0 ? EnumLists.ELogType.AddRecord : EnumLists.ELogType.UpdateRecord,
                        "<strong>[" + ResDatabaseContext.ResourceManager.GetString(typeof(T).Name, new CultureInfo(SystemConfigurationService.GetValue("LogCulture"))) +
                        "]</strong> " +
                        (newId == 0
                            ? (Environment.NewLine +
                                JValue.Parse(entity.RemoveProxies().ToJSON()).ToString(Formatting.Indented))
                            : (Environment.NewLine + "<strong>New Values:</strong> " +
                                Environment.NewLine + " " +
                                (differences.Any() ? string.Join(Environment.NewLine + " ", differences) : "None") //+
                                                                                                                   //Environment.NewLine +
                                                                                                                   //Environment.NewLine + "<strong>Old Values:</strong> " +
                                                                                                                   //Environment.NewLine + JToken.Parse(oldEntityJson).ToString(Formatting.Indented) 
                                                                                                                   //JValue.Parse(entity.RemoveProxies().ToJSON()).ToString(Formatting.Indented)
                            )));
                }

                return result;
            }
            catch (Exception e)
            {
                result.Success = false;
                result.Message = e.Message;
                return result;
            }

        }

        public ResultProcessing Delete(T entity)
        {
            var result = new ResultProcessing();
            try
            {
                var entityLog = entity.RemoveProxies().ToJSON();
                using (var context = new DatabaseContext())
                {
                    context.Entry(entity).State = EntityState.Deleted;
                    context.SaveChanges();
                }
                result.Success = true;

                //Log
                LogService.Save(EnumLists.ELogType.DeleteRecord,
                    "<strong>[" + ResDatabaseContext.ResourceManager.GetString(typeof(T).Name, new CultureInfo(SystemConfigurationService.GetValue("LogCulture"))) +
                    "]</strong> " + Environment.NewLine + JValue.Parse(entityLog).ToString(Formatting.Indented));

                return result;
            }
            catch (Exception e)
            {
                result.Success = false;
                result.Message = e.Message;
                return result;
            }

        }

        public ResultProcessing DeleteRange(ICollection<T> entityList)
        {
            var result = new ResultProcessing();
            try
            {
                using (var context = new DatabaseContext())
                {
                    var entityIdList = entityList.Select(p => p.Id).ToList();
                    var entities = context.Set<T>().Where(g => entityIdList.Contains(g.Id));
                    context.Set<T>().RemoveRange(entities);
                    context.SaveChanges();
                }
                result.Success = true;
                return result;
            }
            catch (Exception e)
            {
                result.Success = false;
                result.Message = e.Message;
                return result;
            }

        }

        public ResultProcessing Delete(int Id)
        {
            var result = new ResultProcessing();
            try
            {
                T entity;
                var entityLog = "";
                using (var context = new DatabaseContext())
                {
                    entity = context.Set<T>().Find(Id);
                    entityLog = entity.RemoveProxies().ToJSON();
                    context.Entry(entity).State = EntityState.Deleted;
                    context.SaveChanges();
                }
                result.Success = true;

                //Log
                LogService.Save(EnumLists.ELogType.DeleteRecord,
                    "<strong>[" + ResDatabaseContext.ResourceManager.GetString(typeof(T).Name, new CultureInfo(SystemConfigurationService.GetValue("LogCulture"))) +
                    "]</strong> " + Environment.NewLine + JValue.Parse(entityLog).ToString(Formatting.Indented));

                return result;
            }
            catch (Exception e)
            {
                result.Success = false;
                result.Message = e.Message;
                return result;
            }

        }


        #region Propriedades

        /// <summary>
        /// Utilizado para incluir foreign keys
        /// </summary>
        public List<string> Includes { get; set; }

        #endregion

        public BaseService()
        {
            Includes = new List<string>();
        }
    }
}
