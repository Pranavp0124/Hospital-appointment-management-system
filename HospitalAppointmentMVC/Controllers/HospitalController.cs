using System.Net.Http.Json;
using Microsoft.Extensions.Caching.Memory;

namespace HospitalAppointmentMVC.Controllers;

public class HospitalController : Controller
{
    // A small list is used instead of a database to keep the project simple.
    private static readonly List<Doctor> doctors = new()
    {
        new Doctor
        {
            DoctorId = 1,
            DoctorName = "Dr. Rahul Sharma",
            Specialization = "Cardiologist",
            Experience = 10,
            ConsultationFee = 800
        },
        new Doctor
        {
            DoctorId = 2,
            DoctorName = "Dr. Neha Patel",
            Specialization = "Dermatologist",
            Experience = 7,
            ConsultationFee = 600
        },
        new Doctor
        {
            DoctorId = 3,
            DoctorName = "Dr. Amit Shah",
            Specialization = "Orthopedic",
            Experience = 12,
            ConsultationFee = 900
        }
    };

    private readonly IMemoryCache _cache;

    public HospitalController(IMemoryCache cache)
    {
        _cache = cache;
    }

    // Home page.
    public IActionResult Index()
    {
        return View(doctors);
    }

    // Doctor details page.
    // The id comes from the query string.
    [ResponseCache(Duration = 180, Location = ResponseCacheLocation.Any)]
    public IActionResult Details(int id)
    {
        string cacheKey = "doctor_" + id;

        // Try to get the doctor from MemoryCache first.
        if (!_cache.TryGetValue(cacheKey, out Doctor? doctor))
        {
            doctor = doctors.FirstOrDefault(d => d.DoctorId == id);

            if (doctor == null)
                return NotFound();

            // Keep this doctor in cache for 3 minutes.
            _cache.Set(cacheKey, doctor, TimeSpan.FromMinutes(3));
        }

        return View(doctor);
    }

    // Show the appointment form.
    public IActionResult Appointment()
    {
        return View();
    }

    // Receive the appointment form.
    [HttpPost]
    public IActionResult Appointment(string patientName, string specialization, int doctorId)
    {
        // Cookie stores the patient's name.
        Response.Cookies.Append("PatientName", patientName);

        // Session stores the selected specialization.
        HttpContext.Session.SetString("Specialization", specialization);

        // The hidden DoctorId is received here.
        TempData["Message"] =
            $"Appointment request received for {patientName}. Doctor ID: {doctorId}";

        return RedirectToAction("Appointment");
    }

    // Read the patient name from cookie and specialization from session.
    public IActionResult AppointmentInfo()
    {
        ViewBag.PatientName = Request.Cookies["PatientName"] ?? "No patient name saved";
        ViewBag.Specialization =
            HttpContext.Session.GetString("Specialization") ?? "No specialization saved";

        return View();
    }

    // Get posts from JSONPlaceholder asynchronously.
    public async Task<IActionResult> Posts()
    {
        using HttpClient client = new HttpClient();

        var posts = await client.GetFromJsonAsync<List<Post>>(
            "https://jsonplaceholder.typicode.com/posts");

        return View(posts ?? new List<Post>());
    }

    // Small class used only for the API response.
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
    }
}
