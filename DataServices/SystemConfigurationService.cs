using Infra;
using Localization.Web;
using Model.AbsModels;
using Model.DatabaseContext;
using System;
using System.Globalization;
using System.Linq;


namespace DataServices
{
    public class SystemConfigurationService : BaseService<SystemConfiguration>
    {

        /// <summary>
        /// Verifica a configuração para os Sites
        /// </summary>
        /// <param name="siteId"></param>
        public void CheckSiteConfiguration()
        {

            //return; // BRITA

            var configurations = new SystemConfiguration().GetDefaultValues();

            var configurationsToDelete = GetAll(filterSite: false).ToList().Where(p => !configurations.Select(c => c.Key).Contains(p.Key)).ToList();
            DeleteRange(configurationsToDelete);

            var sites = new SiteJabilService().GetAll();
            foreach (var site in sites)
            {

                foreach (var configuration in configurations)
                {
                    if (GetByFilter(p => p.Key == configuration.Key && p.SiteId == site.Id, filterSite: false).FirstOrDefault() == null)
                    {
                        Save(new SystemConfiguration { Key = configuration.Key, Value = configuration.Value, SiteId = site.Id }, saveLog: false);
                    }
                }
            }
        }

        /// <summary>
        /// Retorna uma lista formatada para uso com o componente Datatables
        /// </summary>
        /// <param name="parameters"></param>
        public void List(ref TableParameter parameters)
        {

            var parametrosLocal = parameters;
            //var sSearchSemAcento = parametrosLocal.sSearch.RemoverAcentos();

            var dadosFiltrados = GetAll();
            var dadosTotal = dadosFiltrados.Count();

            dadosFiltrados = dadosFiltrados.OrderBy(x => x.Description).ToList();

            //Gerando resultado para ser utilizado no JSon
            //var dadosFiltradosLista = dadosFiltrados as IList<Stock> ?? dadosFiltrados.ToList();
            var switchKeys = new SystemConfiguration().GetDefaultValues().Where(p => p.ConfigurationType == EnumLists.EConfigurationType.Switch).Select(p => p.Key);
            var dadosFinal = dadosFiltrados.Select(x => new[]
            {
                    x.Description,
                    switchKeys.Contains(x.Key) ? (x.Value.Equals("1") ? ResLabels.Enabled : ResLabels.Disabled)
                        : x.Value,
                    string.Format(
                        WebButtons.MakeGroup(
                            WebButtons.EditSm
                        )
                        , x.Id.ToString(CultureInfo.CurrentCulture))
                });

            parametrosLocal.TotalReg = dadosTotal;
            parametrosLocal.FilteredData = dadosFiltrados;
            parametrosLocal.DisplayReg = dadosFiltrados.Count();
            parametrosLocal.FinalData = dadosFinal;

            parameters = parametrosLocal;
        }


        /// <summary>
        /// Retorna uma Configuration com o ID fornecido
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetValue(string key, int siteId = 0)
        {
            var configApp = new SystemConfigurationService();
            siteId = siteId != 0 ? siteId : GlobalFunctions.SiteId;
            var configuration = configApp.GetByFilter(p => p.Key.Equals(key, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            return (configuration ?? new SystemConfiguration { Value = "0" }).Value;

        }

    }
}
