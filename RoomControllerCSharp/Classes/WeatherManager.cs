using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomControllerCSharp.Classes
{
    public static class WeatherManager
    {
        /// <summary>
        /// This is how the NOAA api tracks position for the weather service. This just needs to be obtained once at app startup.
        /// </summary>
        public static int GridSquareX = 0;

        /// <summary>
        /// This is how the NOAA api tracks position for the weather service. This just needs to be obtained once at app startup.
        /// </summary>
        public static int GridSquareY = 0;

        /// <summary>
        /// This is hourly forecast data for the current day. Update this ~once per hour and at app startup.
        /// </summary>
        public static string HourlyForecastJson = "";

        /// <summary>
        /// This is daily forecast data for the current day. Update this ~once per hour and at app startup.
        /// </summary>
        public static string DailyForecastJson = "";

        /// <summary>
        /// Calls all necessary weather data at application startup to make sure everything is populated.
        /// </summary>
        public static async void WeatherStartup()
        {
            await UpdateGridLocationAsync();
            await UpdateHourlyForecastJsonAsync();
            await UpdateDailyForecaseJsonAsync();
        }

        /// <summary>
        /// Updates the GridSquare position with the current location.
        /// </summary>
        public static async Task UpdateGridLocationAsync()
        {
            // Call the api service to get this
            //https://api.weather.gov/points/39.7456,-97.0892
        }

        /// <summary>
        /// Updates the HourlyForecastJson string for the current GridSquares.
        /// </summary>
        public static async Task UpdateHourlyForecastJsonAsync()
        {
            // Call the api service to get this.
            //https://api.weather.gov/gridpoints/TOP/32,81/forecast/hourly
        }

        /// <summary>
        /// Updates the DailyForecastJson string for the current GridSquares.
        /// </summary>
        public static async Task UpdateDailyForecaseJsonAsync()
        {
            // Call the api service to get this.
            //https://api.weather.gov/gridpoints/TOP/31,80/forecast
        }

        /// <summary>
        /// Parse the daily weather json string for the weather string for a specified day.
        /// </summary>
        /// <param name="day">This starts at 0 for today, and add one for each 12-hour segment after that.</param>
        /// <returns></returns>
        public static (string Name, bool IsDay, string ShortForecast, string ForecastDescription, string Temperature) GetWeatherForDay(int day)
        {
            // Implement this.
            return ("", false, "", "", "");
        }
    }
}
