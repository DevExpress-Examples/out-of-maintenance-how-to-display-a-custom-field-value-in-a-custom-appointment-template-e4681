using System;
using System.Windows;
using DevExpress.Xpf.Scheduler;
using DevExpress.XtraScheduler;

namespace SchedulerCustomFieldWpf {
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();

            SchedulerStorage schedulerStorage = schedulerControl1.Storage;
            schedulerStorage.AppointmentStorage.CustomFieldMappings.Add(new SchedulerCustomFieldMapping("cfPrice", "Price"));
            Appointment apt = schedulerStorage.CreateAppointment(AppointmentType.Normal);
            apt.CustomFields["cfPrice"] = 10;
            apt.Start = DateTime.Today.AddHours(1);
            apt.End = DateTime.Today.AddHours(2);
            apt.Subject = "Test";
            schedulerStorage.AppointmentStorage.Add(apt);

            schedulerControl1.Start = DateTime.Today;
            schedulerControl1.DayView.TopRowTime = TimeSpan.Zero;
        }

        private void schedulerControl1_AppointmentViewInfoCustomizing(object sender, AppointmentViewInfoCustomizingEventArgs e) {
            CustomAppointmentData cad = new CustomAppointmentData();
            object price = e.ViewInfo.Appointment.CustomFields["cfPrice"];

            if (price != null && price != DBNull.Value)
                cad.Price = Convert.ToDecimal(price);
            e.ViewInfo.CustomViewInfo = cad;
        }
    }

    public class CustomAppointmentData : DependencyObject {
        public static readonly DependencyProperty PriceProperty = DependencyProperty.Register("Price", typeof(decimal), typeof(CustomAppointmentData), new PropertyMetadata(0m));
        public decimal Price { get { return (decimal)GetValue(PriceProperty); } set { SetValue(PriceProperty, value); } }
    }
}