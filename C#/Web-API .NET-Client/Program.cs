using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace HttpClientSample
{
    public class Car
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Typ { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
    public class Telemetry
    {
        public static long Id { get; set; }
        public long? Latitude { get; set; }
        public long? Longitude { get; set; }
        public int? Speed { get; set; }
        public int? Capacity { get; set; }
        public long CarId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }


    class Program
    {
        static HttpClient client = new HttpClient();
        public static string uri = "https://localhost:7102";

        #region CarAPI
        static void ShowCar(Car cars)
        {
            Console.WriteLine($"Name: {cars.Name}\tTyp: " +
                $"{cars.Typ}\tCreatedAt: {cars.CreatedAt}\tModifiedAt: " +
                $"{cars.ModifiedAt}");
        }

        static async Task<Uri> CreateCarAsync(Car cars)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(
                "api/Cars", cars);
            response.EnsureSuccessStatusCode();

            // return URI of the created resource.
            return response.Headers.Location;
        }

        static async Task<Car> GetCarAsync(string path)
        {
            Car product = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                product = await response.Content.ReadAsAsync<Car>();
            }
            return product;
        }

        static async Task<Car> UpdateCarAsync(Car product)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync(
                $"api/Cars/{product.Id}", product);
            response.EnsureSuccessStatusCode();

            // Deserialize the updated product from the response body.
            product = await response.Content.ReadAsAsync<Car>();
            return product;
        }

        static async Task<HttpStatusCode> DeleteCarAsync(string id)
        {
            HttpResponseMessage response = await client.DeleteAsync(
                $"api/Cars/{id}");
            return response.StatusCode;
        }
        #endregion

        #region TelemetryAPI
        static void ShowTelemetry(Telemetry telemetry)
        {
            Console.WriteLine($"Lat: {telemetry.Latitude}\t" +
                $"Lon: {telemetry.Longitude}\tSpeed: {telemetry.Speed}\t" +
                $"Cap: {telemetry.Capacity}\tCarId: {telemetry.CarId}\t" +
                $"CreatedAt: {telemetry.CreatedAt}\tModifiedAt: " +
                $"{telemetry.ModifiedAt}");
        }

        static async Task<Uri> CreateTelemetryAsync(Telemetry telemetry)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(
                "api/Telemetries", telemetry);
            response.EnsureSuccessStatusCode();

            // return URI of the created resource.
            return response.Headers.Location;
        }

        static async Task<Telemetry> GetTelemetryAsync(string path)
        {
            Telemetry telemetry = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                telemetry = await response.Content.ReadAsAsync<Telemetry>();
            }
            return telemetry;
        }

        static async Task<Telemetry> UpdateTelemetryAsync(Telemetry telemetry)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync(
                $"api/Telemetries/{Telemetry.Id}", telemetry);
            response.EnsureSuccessStatusCode();

            // Deserialize the updated product from the response body.
            telemetry = await response.Content.ReadAsAsync<Telemetry>();
            return telemetry;
        }

        static async Task<HttpStatusCode> DeleteTelemetryAsync(string id)
        {
            HttpResponseMessage response = await client.DeleteAsync(
                $"api/Telemetries/{id}");
            return response.StatusCode;
        }
        #endregion

        static void Main()
        {
            RunAsync().GetAwaiter().GetResult();
        }

        static async Task RunAsync()
        {
            // Update port # in the following line.
            client.BaseAddress = new Uri(uri);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                // Create a new cars
                Car cars = new Car
                {
                    Name = "Audi",
                    Typ = "A4"
                };

                Telemetry telemetry = new Telemetry
                {
                    Latitude = 1,
                    Longitude = 2,
                    Speed = 12,
                    Capacity = 12,
                    CarId = 2
                };

                #region CarAPIRequest
                var url1 = await CreateCarAsync(cars);
                Console.WriteLine($"Created at {url1}");

                // Get the cars
                cars = await GetCarAsync(url1.PathAndQuery);
                ShowCar(cars);

                /*
                // Update the cars
                Console.WriteLine("Updating Tpy...");
                cars.Typ = "UpTyp";
                await UpdateCarAsync(cars);
                */

                /*
                // Get the updated cars
                product = await GetCarAsync(url.PathAndQuery);
                ShowCar(cars);
                */

                /*
                // Delete the cars
                var statusCode = await DeleteCarAsync(product.Id.ToString());
                Console.WriteLine($"Deleted (HTTP Status = {(int)statusCode})");
                */
                #endregion

                #region TelemetryAPIRequest

                var url2 = await CreateTelemetryAsync(telemetry);
                Console.WriteLine($"Created at {url2}");

                // Get the telemetry
                telemetry = await GetTelemetryAsync(url2.PathAndQuery);
                ShowTelemetry(telemetry);

                /*
                // Update the telemetry
                Console.WriteLine("Updating Tpy...");
                telemetry.Speed = 88;
                await UpdateTelemetryAsync(telemetry);
                //*/

                /*
                // Get the updated telemetry
                telemetry = await GetCarAsync(url.PathAndQuery);
                ShowCar(telemetry);
                */

                /*
                // Delete the cars
                var statusCode = await DeleteTelemetryAsync(product.Id.ToString());
                Console.WriteLine($"Deleted (HTTP Status = {(int)statusCode})");
                */

                #endregion

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }
    }
}