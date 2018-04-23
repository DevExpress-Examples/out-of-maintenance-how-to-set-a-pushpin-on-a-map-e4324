using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using DevExpress.UI.Xaml.Map;

namespace PushpinSample {

    public sealed partial class MainPage : Page {

        public static DependencyProperty PointBLocationProperty =
            DependencyProperty.Register("PointBLocation", typeof(GeoPoint), typeof(MainPage), new PropertyMetadata(new GeoPoint()));

        public GeoPoint PointBLocation {
            get {
                return (GeoPoint)GetValue(PointBLocationProperty);
            }
            set {
                SetValue(PointBLocationProperty, (GeoPoint)value);
            }
        }

        public MainPage() {
            this.InitializeComponent();
            DataContext = this;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e) {
        }

        private void slLat_ValueChanged(object sender, RangeBaseValueChangedEventArgs e) {
            PointBLocation = new GeoPoint(slLat.Value, slLon.Value);
        }

        private void MapControl_PointerPressed(object sender, PointerRoutedEventArgs e) {
            if (e.KeyModifiers == Windows.System.VirtualKeyModifiers.Control)
                CreatePushPin(e.GetCurrentPoint(map).Position);
        }

        void CreatePushPin(Point hitPoint) {
            VectorItemsLayer layer = (VectorItemsLayer)map.Layers[1];
            GeoPoint pos = layer.ScreenToGeoPoint(hitPoint);
            int number = layer.Items.Count + 1;
            string txt = number.ToString();
            string positionStr = string.Format("#{0} ({1:n2}, {2:n2})", number, pos.Latitude, pos.Longitude);
            layer.Items.Add(new MapPushpin() { Text = txt, Location = pos, ToolTipPattern = positionStr });
        }
    }
}
