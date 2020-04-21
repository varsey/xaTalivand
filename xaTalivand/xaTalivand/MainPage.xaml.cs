using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace xaTalivand
{
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public class RootObject
        {
            public List<Dict> dict { get; set; }
        }
        public class Dict
        {
            public string ID { get; set; }
            public string Term { get; set; }
            public string Translation { get; set; }
            public string Comment { get; set; }
            public string Contributor { get; set; }
            public string Editor { get; set; }
            public string Fin { get; set; }
        }
        public MainPage()
        {
            InitializeComponent();
        }

        [Obsolete]
        public void OnClickGoButton(object sender, EventArgs e)
        {
            var City = CityName.Text;
            var RC = new RestClient();
            var Request = new RestRequest("http://talivandr.site/db/talivandr_db.json");

            RC.ExecuteAsyncGet(Request, (IRestResponse response, RestRequestAsyncHandle arg2) =>
            {
                var Data = JsonConvert.DeserializeObject<Dict>(response.Content);
                
                //var icon = Data.ToString();

                MainThread.BeginInvokeOnMainThread(() =>
                {
                    Temperature.Text = City.ToString() + " :"; // + icon.ToString();
                    CityName.Text = "";
                    CityName.IsEnabled = true;
                });
            }, "GET");

            CityName.Text = "Загрузка..."; //код ассинхр, поэтому эта часть выполниться раньше чем запрос на сервер
            CityName.IsEnabled = false;
        }
    }
}
