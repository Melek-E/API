import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {
  private hubConnection: signalR.HubConnection | undefined;

  constructor() {}

  // Initialize the connection to the SignalR hub
  public startConnection = () => {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('http://localhost:7112/testNotificationHub') // Match this with your backend SignalR hub URL
      .build();

    this.hubConnection
      .start()
      .then(() => console.log('SignalR connection started'))
      .catch(err => console.error('Error while starting SignalR connection: ' + err));
  }

  // Subscribe to the notifications (e.g., when a new test is created)
  public addReceiveTestNotificationListener = (callback: (message: string) => void) => {
    if (this.hubConnection) {
      this.hubConnection.on('ReceiveTestNotification', (message: string) => {
        callback(message);  // Call the provided callback with the message
      });
    }
  }
}
