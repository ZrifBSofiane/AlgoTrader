using System.Web;
using System.Web.Optimization;

namespace Web
{
    public class BundleConfig
    {
        // Pour plus d'informations sur le regroupement, visitez https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                      "~/Content/files/bower_components/jquery/js/jquery.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery-ui").Include(
                       "~/Content/files/bower_components/jquery/js/jquery.min.js",
                       "~/Content/files/bower_components/jquery-ui/js/jquery-ui.min.js",
                       "~/Content/files/bower_components/popper.js/js/popper.min.js",
                       "~/Content/files/bower_components/bootstrap/js/bootstrap.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/components").Include(
                       "~/Content/files/bower_components/jquery-slimscroll/js/jquery.slimscroll.js",
                       "~/Content/files/bower_components/modernizr/js/modernizr.js",
                       "~/Content/files/bower_components/modernizr/js/css-scrollbars.js",
                       "~/Content/files/bower_components/chart.js/js/Chart.js",
                       "~/Content/files/assets/pages/google-maps/gmaps.js",
                       "~/Content/files/assets/pages/widget/gauge/gauge.min.js",
                       "~/Content/files/assets/pages/widget/amchart/amcharts.j.js",
                       "~/Content/files/assets/pages/widget/amchart/serial.js",
                       "~/Content/files/assets/pages/widget/amchart/gauge.js",
                       "~/Content/files/assets/js/pcoded.min.js",
                       "~/Content/files/assets/js/vartical-layout.min.js",
                       "~/Content/files/assets/js/jquery.mCustomScrollbar.concat.min.js",
                       "~/Content/files/assets/pages/dashboard/crm-dashboard.min.js",
                       "~/Content/files/assets/pages/js/script.js",
                       "~/Content/files/assets/js/script.js"));



            // Utilisez la version de déassets\pages\widget\amchart\pie.js"></veloppement de Modernizr pour le développement et l'apprentissage. Puis, une fois
            // prêt pour la production, assets\pages\widget\amchart\light.js">utilisez l'outil de génération à l'adresse https://modernizr.com pour sélectionner uniquement les tests dont vous avez besoin.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/files/bower_components/bootstrap/css/bootstrap.min.css",
                      "~/Content/files/assets/pages/chart/radial/css/radial.css",
                      "~/Content/files/assets/icon/feather/css/feather.css",
                      "~/Content/files/assets/css/style.css",
                      "~/Content/files/assets/css/jquery.mCustomScrollbar.css"));
        }
    }
}
