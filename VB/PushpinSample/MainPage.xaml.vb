Imports Windows.Foundation
Imports Windows.UI.Xaml
Imports Windows.UI.Xaml.Controls
Imports Windows.UI.Xaml.Controls.Primitives
Imports Windows.UI.Xaml.Input
Imports Windows.UI.Xaml.Navigation
Imports DevExpress.UI.Xaml.Map

Public NotInheritable Class MainPage
    Inherits Page

    Public Shared PointBLocationProperty As DependencyProperty = _
        DependencyProperty.Register("PointBLocation", GetType(GeoPoint), GetType(MainPage), New PropertyMetadata(New GeoPoint()))

    Public Property PointBLocation() As GeoPoint
        Get
            Return CType(GetValue(PointBLocationProperty), GeoPoint)
        End Get
        Set(ByVal value As GeoPoint)
            SetValue(PointBLocationProperty, CType(value, GeoPoint))
        End Set
    End Property

    Public Sub New()
        InitializeComponent()
        DataContext = Me
    End Sub

    Protected Overrides Sub OnNavigatedTo(e As Navigation.NavigationEventArgs)
    End Sub

    Private Sub slLat_ValueChanged(ByVal sender As Object, ByVal e As RangeBaseValueChangedEventArgs)
        PointBLocation = New GeoPoint(slLat.Value, slLon.Value)
    End Sub

    Private Sub MapControl_PointerPressed(ByVal sender As Object, ByVal e As PointerRoutedEventArgs)
        If e.KeyModifiers = Windows.System.VirtualKeyModifiers.Control Then
            CreatePushPin(e.GetCurrentPoint(map).Position)
        End If
    End Sub

    Private Sub CreatePushPin(ByVal hitPoint As Point)
        Dim layer As VectorItemsLayer = CType(map.Layers(1), VectorItemsLayer)
        Dim pos As GeoPoint = layer.ScreenToGeoPoint(hitPoint)
        Dim number As Integer = layer.Items.Count + 1
        Dim txt As String = number.ToString()
        Dim positionStr As String = String.Format("#{0} ({1:n2}, {2:n2})", number, pos.Latitude, pos.Longitude)
        layer.Items.Add(New MapPushpin() With {.Text = txt, .Location = pos, .ToolTipPattern = positionStr})
    End Sub

End Class
