# How to display a custom field value in a custom appointment template


<p>This example illustrates how to display a custom appointment field value (see <a href="http://documentation.devexpress.com/#WPF/CustomDocument8917">How to: Create a Custom Field for an Appointment</a>) in the appointment rectangle. We override the appointment template for this purpose in the following manner:</p><p></p>

```xaml
    <DataTemplate x:Key="{dxscht:SchedulerViewThemeKey ResourceKey=VerticalAppointmentContentTemplate}">
        <Grid SnapsToDevicePixels="True" HorizontalAlignment="Stretch" VerticalAlignment="Stretch" Background="Transparent">
            ...
            <TextBlock Text="{Binding CustomViewInfo.Price, StringFormat={}{0:C2}}" Foreground="Red" TextTrimming="CharacterEllipsis"/>
            ...
        </Grid>
    </DataTemplate>
```

<p></p><p>To pass a custom field value via the <strong>CustomViewInfo</strong>, handle the <a href="http://documentation.devexpress.com/#WPF/DevExpressXpfSchedulerSchedulerControl_AppointmentViewInfoCustomizingtopic">SchedulerControl.AppointmentViewInfoCustomizing Event</a> as follows:</p><p></p>

```cs
        private void schedulerControl1_AppointmentViewInfoCustomizing(object sender, AppointmentViewInfoCustomizingEventArgs e) {
            CustomAppointmentData cad = new CustomAppointmentData();
            object price = e.ViewInfo.Appointment.CustomFields["cfPrice"];

            if (price != null && price != DBNull.Value)
                cad.Price = Convert.ToDecimal(price);
            e.ViewInfo.CustomViewInfo = cad;
        }
```

<p></p><p>The <strong>CustomAppointmentData </strong>class is defined as follows:</p><p></p>

```cs
    public class CustomAppointmentData : DependencyObject {
        public static readonly DependencyProperty PriceProperty = DependencyProperty.Register("Price", typeof(decimal), typeof(CustomAppointmentData), new PropertyMetadata(0m));
        public decimal Price { get { return (decimal)GetValue(PriceProperty); } set { SetValue(PriceProperty, value); } }
    }
```

<p></p><p>Note that this approach is also used in the "Appointment Customization" demo module (see <a href="http://documentation.devexpress.com/#WPF/CustomDocument8647">Demos in Installation</a>). In addition, the <strong>HorizontalAppointmentContentTemplate</strong> in this demo is customized. It affects the appointment appearance in views other than <strong>Day </strong>and <strong>Work Week</strong>.</p><p></p><p>Here is a screenshot that illustrates a sample application in action:</p><p></p><p><img src="https://raw.githubusercontent.com/DevExpress-Examples/how-to-display-a-custom-field-value-in-a-custom-appointment-template-e4681/12.2.8+/media/c819a21f-1ddc-4a6f-87d7-e06d56250770.png"></p><p></p><p><strong>See Also:</strong></p><p><a href="https://www.devexpress.com/Support/Center/p/E4432">How to display custom tooltips for appointments, resource headers and date headers</a></p>

<br/>


