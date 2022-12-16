Imports System
Imports System.Windows
Imports DevExpress.Xpf.Scheduler
Imports DevExpress.XtraScheduler

Namespace SchedulerCustomFieldWpf

    Public Partial Class MainWindow
        Inherits Window

        Public Sub New()
            Me.InitializeComponent()
            Dim schedulerStorage As SchedulerStorage = Me.schedulerControl1.Storage
            schedulerStorage.AppointmentStorage.CustomFieldMappings.Add(New SchedulerCustomFieldMapping("cfPrice", "Price"))
            Dim apt As Appointment = schedulerStorage.CreateAppointment(AppointmentType.Normal)
            apt.CustomFields("cfPrice") = 10
            apt.Start = Date.Today.AddHours(1)
            apt.End = Date.Today.AddHours(2)
            apt.Subject = "Test"
            schedulerStorage.AppointmentStorage.Add(apt)
            Me.schedulerControl1.Start = Date.Today
            Me.schedulerControl1.DayView.TopRowTime = TimeSpan.Zero
        End Sub

        Private Sub schedulerControl1_AppointmentViewInfoCustomizing(ByVal sender As Object, ByVal e As AppointmentViewInfoCustomizingEventArgs)
            Dim cad As CustomAppointmentData = New CustomAppointmentData()
            Dim price As Object = e.ViewInfo.Appointment.CustomFields("cfPrice")
            If price IsNot Nothing AndAlso price IsNot DBNull.Value Then cad.Price = Convert.ToDecimal(price)
            e.ViewInfo.CustomViewInfo = cad
        End Sub
    End Class

    Public Class CustomAppointmentData
        Inherits DependencyObject

        Public Shared ReadOnly PriceProperty As DependencyProperty = DependencyProperty.Register("Price", GetType(Decimal), GetType(CustomAppointmentData), New PropertyMetadata(0D))

        Public Property Price As Decimal
            Get
                Return CDec(GetValue(PriceProperty))
            End Get

            Set(ByVal value As Decimal)
                SetValue(PriceProperty, value)
            End Set
        End Property
    End Class
End Namespace
