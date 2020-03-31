using BoundaryDetector.Models;
using BoundaryDetector.Persistence;
using BoundaryDetector.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Maps;

namespace BoundaryDetector.ViewModels
{
    public class GFieldViewModel : BaseViewModel
    {
        private readonly GFieldRepository _gFieldRepository;
        private readonly IPageService _pageService;
        private readonly RestService _restService;
        public Map Map { get; set; }
        public IList<Position> Polygon { get; set; }
        public GField GField { get; private set; }
        public ObservableCollection<GField> GFields { get; private set; }
        public ICommand SaveCommand { get; set; }
        public ICommand ShowFieldCommand { get; set; }
        public ICommand ClearMapCommand { get; set; }
        public ICommand PerformSearchCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand UpdateCommand { get; set; }
        public GFieldViewModel()
        {
            Map = new Map(new MapSpan(new Position(51.1657, 10.4515),10,10));
            Map.MapType = MapType.Street;
            Map.HasScrollEnabled = true;
            Map.HasZoomEnabled = true;
            Map.IsShowingUser = true;
            Map.MoveToLastRegionOnLayoutChange = false;
            Map.MapClicked += MapClicked;

            _gFieldRepository = new GFieldRepository(DependencyService.Get<ISQLiteDb>());
            _pageService = new PageService();
            _restService = new RestService();


            SaveCommand = new Command(async () => await Save());
            ShowFieldCommand = new Command((args) => SelectField((GField)args));
            ClearMapCommand = new Command(() => ClearMap());
            DeleteCommand = new Command(async args => await Delete((GField)args));
            UpdateCommand = new Command(async args => await Update((GField)args));
            PerformSearchCommand = new Command((searchText) => { Search(null,(string)searchText); });
            GFields = new ObservableCollection<GField>();


            MessagingCenter.Subscribe<MainPage, string>(this, Events.SearchBarTextChanged, Search);
            
        }

        private async Task Delete(GField gField)
        {
            await _gFieldRepository.Delete(gField);
            GFields.Remove(gField);
        }

        private async Task Update(GField gField)
        {
            await _gFieldRepository.Update(gField);
        }

        private async void Search(MainPage mainPage,string searchText)
        {
            if (String.IsNullOrWhiteSpace(searchText))
            {
                GFields.Clear();
            }
            else
            {
                var result = await _gFieldRepository.GetAsync(x => x.FieldName.Contains(searchText));
                GFields.Clear();
                result.ForEach(x =>
                {
                    GFields.Add(x);
                });
            }
        }

        private void ClearMap()
        {
            var count = Map?.MapElements?.Count;
            for (int i = 0; i < count; i++)
            {
                Map.MapElements.RemoveAt(0);
            }
            MessagingCenter.Send(this, Events.ClearMapClicked);
        }

        private async void MapClicked(object sender, MapClickedEventArgs e)
        {
            var anyResult = await GFieldRequest(e.Position.Longitude, e.Position.Latitude);
            MessagingCenter.Send(this, Events.MapClicked, anyResult);
        }

        private void SelectField(GField gField)
        {
            Polygon polygon = new Polygon
            {
                StrokeWidth = 7,
                StrokeColor = (Color) Application.Current.Resources["Primary"],
                FillColor = Color.Transparent
            };
            IList<Position> positions = gField.BoundaryDetails?.features[0]?.geometry?.coordinates[0]?
                .Select(cor => new Position(cor[1], cor[0])).ToList();
            positions?.ForEach(p =>
            {
                polygon.Geopath.Add(p);

            });
            ClearMap();
            Map?.MoveToRegion(new MapSpan(positions[0], 0.1, 0.1));
            Map?.MapElements.Add(polygon);
            currentPolygon = polygon;
            MessagingCenter.Send(this, Events.FieldSelected);
        }

        private Polygon currentPolygon;
        public async Task<bool> GFieldRequest(double longitude, double latitude)
        {
            BoundaryDetails boundaryDetails = await _restService.Post(longitude, latitude);

            if (boundaryDetails == null)
            {
                return false;
            }
            GField = new GField
            {
                FieldName = "",
                BoundaryDetails = boundaryDetails
            };
            GField.FieldName = "";

            SelectField(GField);
            return true;
        }

        private async Task Save()
        {
            if (String.IsNullOrWhiteSpace(GField.FieldName))
            {
                await _pageService.DisplayAlert("Error", "Please enter the name.", "OK");
            }
            else
            {
                await _gFieldRepository.Add(GField);
                MessagingCenter.Send(this, Events.GFiledAdded);
            }
            
        }
    }
}
