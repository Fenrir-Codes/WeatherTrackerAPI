import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class WeatherService {
  private apiUrl = 'https://localhost:7271/api/weather';
  private weatherDataSubject = new BehaviorSubject<any>(null);
  weatherData = this.weatherDataSubject.asObservable();
  private skip: number = 0;
  private take: number = 6;

  private alertMessageSubject = new BehaviorSubject<string | null>(null);
  alertMessage = this.alertMessageSubject.asObservable();

  private forecastDataSubject = new BehaviorSubject<any[]>([]);
  forecastData = this.forecastDataSubject.asObservable();

  //#region Send alert
  sendAlert(message: string) {
    this.alertMessageSubject.next(message);
  }
  //#endregion

  //#region Set City name
  async setCity(city: string): Promise<void> {
    try {
      const response = await fetch(`${this.apiUrl}/${city}`);
      if (!response.ok) {
        throw new Error('Error fetching data');
      }

      const data = await response.json();
      this.weatherDataSubject.next(data);
      //console.log(data)
    } catch (error) {
      //console.error('Error fetching weather data:', error);
      this.sendAlert('Error while downloading weather data.');
      this.weatherDataSubject.next(null);
    }
  }
  //#endregion

  //#region Get weather History from database
  async getWeatherHistory(isInitialLoad: boolean = false): Promise<any[]> {
    try {
      if (isInitialLoad) {
        this.skip = 0; // If we load first time we make it empty
      }

      const response = await fetch(`${this.apiUrl}/history?skip=${this.skip}&take=${this.take}`);

      if (!response.ok) {
        const error = await response.json();
        this.sendAlert("Error fetching weather history." + error);
      }

      const weatherHistory: any[] = await response.json();

      if (weatherHistory.length > 0) {
        this.skip += this.take; // Raising the "skip" value for the next loading
      }

      //console.log(weatherHistory)
      return weatherHistory;

    } catch (error) {
      this.sendAlert('Error while downloading weather history.');
      throw error;
    }
  }
  //#endregion

  //#region Search the weather history in database
async searchWeatherHistory(city: string): Promise<any[]> {
  try {
    const response = await fetch(`${this.apiUrl}/searchinhistory?city=${city}`);

    if (!response.ok) {
      const error = await response.json();
      this.sendAlert('Error searching weather history.' + error);
    }

    const weatherHistory: any[] = await response.json();
    return weatherHistory;

  } catch (error) {
    this.sendAlert('Error in searchWeatherHistory');
    throw error;
  }
}
  //#endregion

  //#region Get forecast for the next 5 days
  async getForecast(city: string): Promise<void> {
    try {
      const response = await fetch(`${this.apiUrl}/forecast?city=${city}`);
      if (!response.ok) throw new Error('Error fetching forecast data');

      const data = await response.json();
      // console.log('Full forecast list:', data.list);

      const dailyForecast = this.filterDailyForecast(data.list);
      console.log("Filtered Forecast Data:", dailyForecast);

      this.forecastDataSubject.next(dailyForecast);

      if (dailyForecast.length < 0) {
        this.sendAlert('Can not get the 5 days forecast for this city.');
      }

    } catch (error) {
      this.sendAlert('Error in getting data for the forecast.');
      // console.error('Error in getForecast:', error);
      this.forecastDataSubject.next([]);
    }
  }
  //#endregion

  //#region Filtered forecast data
  private filterDailyForecast(forecastList: any[]): any[] {
    const dailyForecast: any[] = [];
    const dates: Set<string> = new Set();

    for (const item of forecastList) {
      const date = item.dt_txt.split(" ")[0];

      // If there is Ha még nincs ilyen dátum a listában, akkor hozzáadjuk
      if (!dates.has(date)) {
        dates.add(date);

        // rounding the wind value
        if (item.wind && item.wind.speed !== undefined) {
          item.wind.speed = Math.round(item.wind.speed);
        }

        dailyForecast.push(item);
      }

      // If we reach 5 dayr we break
      if (dailyForecast.length === 5) break;
    }

    if (dailyForecast.length === 0) {
      this.sendAlert("Forecast is not possible for this city.");
    }

    return dailyForecast;
  }
  //#endregion

  //#region Reset the skip value
  resetSkip(): void {
    this.skip = 0;
  }
  //#endregion

  //#region Clean the data if necessary
  clearWeatherData(): void {
    this.weatherDataSubject.next(null);  // Clear data
  }

  clearForecastData(): void {
    this.forecastDataSubject.next([]);
  }
  //#endregion

}
