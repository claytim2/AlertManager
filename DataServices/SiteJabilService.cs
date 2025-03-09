using Model.AbsModels;
using Model.JmdDataServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Configuration;

namespace DataServices
{


    public class SiteJabilService
    {
        JmdODataService.Container _container;

        public SiteJabilService()
        {
            _container = new JmdODataService.Container(new Uri(WebConfigurationManager.AppSettings["jmdodatawebapi"]));
        }

        /// <summary>
        /// Lista todos os registros da tabela
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SiteJabil> GetAll()
        {
            var sites = _container.Factories;
            var siteList = new List<SiteJabil>();
            foreach (var site in sites)
            {
                siteList.Add(
                    new SiteJabil
                    {
                        Id = site.Id,
                        Description = site.Name
                    }
                );
            }
            return siteList;
        }

        public IEnumerable<SiteJabil> GetByFilter(Expression<Func<JmdODataService.Factory, bool>> filter)
        {
            var sites = _container.Factories.Where(filter);
            var siteList = new List<SiteJabil>();
            foreach (var site in sites)
            {
                siteList.Add(
                    new SiteJabil
                    {
                        Id = site.Id,
                        Description = site.Name
                    }
                );
            }
            return siteList;
        }

        public int Count(Expression<Func<JmdODataService.Factory, bool>> filter = null)
        {
            IQueryable<JmdODataService.Factory> sites;
            if (filter != null)
                sites = _container.Factories.Where(filter);
            else
                sites = _container.Factories;

            return sites.ToList().Count();
        }

        public SiteJabil GetById(int Id)
        {
            var siteJmd = _container.Factories.Where(p => p.Id == Id).FirstOrDefault();
            SiteJabil site = null;
            if (siteJmd != null)
            {
                site = new SiteJabil { Id = siteJmd.Id, Description = siteJmd.Name };
            }

            return site;
        }

        /// <summary>
        /// Retorna uma lista formatada para uso com o componente Datatables
        /// </summary>
        /// <param name="parameters"></param>
        public void List(ref TableParameter parameters)
        {

            var dadosTotal = Count();
            var parametrosLocal = parameters;
            //var sSearchSemAcento = parametrosLocal.sSearch.RemoverAcentos();

            //Filtro dos dados através do campo de busca

            var siteList = new List<SiteJabil>();

            foreach (var site in _container.Factories)
            {
                siteList.Add(new SiteJabil { Id = site.Id, Description = site.Name });
            }

            Func<SiteJabil, object> orderingFunction;
            switch (parametrosLocal.iSortCol_0)
            {
                case "1":
                    orderingFunction = (x => x.Description);
                    break;
                default:
                    orderingFunction = (x => x.Id);
                    break;
            }

            var dadosFiltrados = siteList
                .Where(x =>
                    string.IsNullOrEmpty(parametrosLocal.sSearch) ||
                    x.Id.ToString().Equals(parametrosLocal.sSearch) ||
                    x.Description.ToUpper().Contains(parametrosLocal.sSearch.ToUpper()))
                .ToList();

            if (parametrosLocal.sSortDir_0 == "asc")
                dadosFiltrados = dadosFiltrados.OrderBy(orderingFunction).ToList();
            else
                dadosFiltrados = dadosFiltrados.OrderByDescending(orderingFunction).ToList();

            dadosFiltrados = dadosFiltrados
                .Skip(parametrosLocal.iDisplayStart)
                .Take(parametrosLocal.iDisplayLength)
                .ToList();


            //Gerando resultado para ser utilizado no JSon
            //var dadosFiltradosLista = dadosFiltrados as IList<Stock> ?? dadosFiltrados.ToList();
            var dadosFinal = dadosFiltrados.Select(x => new[]
            {
                    x.Id.ToString(),
                    x.Description
            });

            parametrosLocal.TotalReg = dadosTotal;
            parametrosLocal.FilteredData = dadosFiltrados;
            parametrosLocal.DisplayReg = dadosFiltrados.Count();
            parametrosLocal.FinalData = dadosFinal;

            parameters = parametrosLocal;
        }
    }
}
