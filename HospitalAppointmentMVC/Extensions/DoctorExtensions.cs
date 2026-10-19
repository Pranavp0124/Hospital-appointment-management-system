namespace HospitalAppointmentMVC.Extensions;

// This extension class contains an extra method for Doctor.
public static class DoctorExtensions
{
    // Add a fixed service charge of Rs.100 to the consultation fee.
    public static decimal CalculateTotalCharge(this Doctor doctor)
    {
        return doctor.ConsultationFee + 100;
    }
}
