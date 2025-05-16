import { Component, OnInit, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { WeatherService } from '../../services/weather.service';
import { AlertBoxComponent } from '../AlertBox/alertBox.component';

@Component({
  selector: 'app-history',
  standalone: true,
  imports: [CommonModule, AlertBoxComponent],
  templateUrl: './history.component.html',
  styleUrls: ['./history.component.css'],
})
export class HistoryComponent implements OnInit {
  @ViewChild('alertBox') alertBox!: AlertBoxComponent;
  weatherHistory: any[] = [];
  loadMoreDisabled: boolean = false;
  loadMoreText: string = 'Load More';

  constructor(private weatherService: WeatherService) { }

  ngOnInit(): void {
    this.loadHistory(true);
  }

  async loadHistory(isInitialLoad: boolean = false): Promise<void> {
    try {
      const historyData = await this.weatherService.getWeatherHistory(isInitialLoad);

      if (isInitialLoad) {
        this.weatherHistory = historyData;
      } else {
        this.weatherHistory = [...this.weatherHistory, ...historyData];
      }

      if (historyData.length < 6) {
        this.loadMoreDisabled = true;
        this.loadMoreText = 'No More Data to load';
      } else {
        this.loadMoreText = 'Load More';
      }
    } catch (error) {
      this.alertBox.showMessage('Error loading weather history.');
    }
  }

async onWeatherSearch(event: Event): Promise<void> {
  const inputElement = event.target as HTMLInputElement;
  const city = inputElement.value.trim();

  try {
    const searchResults = await this.weatherService.searchWeatherHistory(city);
    this.weatherHistory = searchResults;

    if (searchResults.length === 0) {
      this.alertBox.showMessage('No records found for the specified city.');
    }
  } catch (error) {
    this.alertBox.showMessage('Error searching weather history.');
  }
}


}
