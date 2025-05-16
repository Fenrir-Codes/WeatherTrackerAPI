import { Component, ViewChild } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { WeatherService } from './services/weather.service';
import { AlertBoxComponent } from "./pages/AlertBox/alertBox.component";

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    RouterModule,
    CommonModule,
    FormsModule,
    AlertBoxComponent
  ],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  @ViewChild('alertBox') alertBox!: AlertBoxComponent;
  isEmpty: boolean = true;
  searchCity: string = '';

  constructor(private weatherService: WeatherService) { }

  // A változó frissítése az inputban beírt értékkel
  onSearcFieldChange() {
    this.isEmpty = !this.searchCity.trim();
  }

  onHomeClicked() {
    this.searchCity = '';
    this.weatherService.clearWeatherData();
    this.weatherService.clearForecastData();
  }

  onSearchClicked(): void {
    if (this.searchCity.trim()) {
      this.weatherService.setCity(this.searchCity.trim());
       this.weatherService.getForecast(this.searchCity);
    }
  }

}
