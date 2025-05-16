import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-alert-box',
  standalone: true,
  imports: [CommonModule],  // ide kell a CommonModule, hogy *ngIf működjön
  templateUrl: './alertbox.component.html',
})
export class AlertBoxComponent {
  message: string = '';
  show: boolean = false;

  showMessage(msg: string, duration: number = 3000): void {
    this.message = msg;
    this.show = true;

    setTimeout(() => {
      this.show = false;
      this.message = '';
    }, duration);
  }
}
