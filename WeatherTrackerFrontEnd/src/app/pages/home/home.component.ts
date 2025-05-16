import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { WeatherService } from '../../services/weather.service';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  weatherData: any = {};
  showWeatherCard: boolean = false;
  showForecastCards: boolean = false;
  forecastData: any[] = [];
  textOnScreen: string = "WeatherTracker is a weather monitoring tool that lets you search for current weather information by city, including temperature, humidity, wind speed, and more. You can also view search history to revisit previously fetched weather data along with the date and time of each query.";

  constructor(private weatherService: WeatherService) { }

  ngOnInit(): void {
    this.weatherService.weatherData.subscribe((data) => {
      if (data) {
        this.weatherData = data;
        this.showWeatherCard = true;
      } else {
        this.showWeatherCard = false;
      }
    });

    this.weatherService.forecastData.subscribe((data) => {
      if (data && data.length > 0) {
        this.forecastData = data;
        this.showForecastCards = true;
      } else {
        this.showForecastCards = false;
      }
    });

  }

}
