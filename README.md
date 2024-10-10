# ChatApplication

ChatApplication is a real-time messaging application built using ASP.NET MVC, SignalR, and .NET 8. This project enables users to communicate with each other in real-time, with conversations stored in a database.

## Features

- Real-time messaging using SignalR
- User authentication and login
- Chat with multiple users
- Display of past messages from the database
- Responsive and user-friendly interface
- Built using ASP.NET MVC and Razor Pages
- APIs created with ASP.NET Web API
- Blazor WebAssembly integration (future implementation)

## Technologies Used

- **ASP.NET MVC**: For building the web application structure and UI
- **ASP.NET Web API**: For creating APIs to handle server-side logic
- **SignalR**: For real-time communication
- **Entity Framework Core**: For database interactions
- **Blazor WebAssembly**: (Planned for future implementation)
- **PostgreSQL**: Database management system
- **.NET 8**: The latest version of .NET framework

## Getting Started

Follow these instructions to set up the ChatApplication on your local machine.

### Prerequisites

- .NET 8 SDK installed
- Visual Studio or any other .NET-compatible IDE
- PostgreSQL or any other supported database
- Git for version control

### Installation

1. **Clone the repository**:
   ```bash
   git clone https://github.com/yourusername/ChatApplication.git
   cd ChatApplication
   ```

2. **Set up the database**:
   - Update the connection string in `appsettings.json` to point to your local database.

3. **Run database migrations**:
   ```bash
   dotnet ef database update
   ```

4. **Build and run the application**:
   ```bash
   dotnet build
   dotnet run
   ```

5. **Access the application**:
   Open your browser and go to `https://localhost:7096`.

### Usage

- **Login**: Users can log in with their credentials to start chatting.
- **Chat with Users**: Click on a user's name to open a chat window and start messaging.
- **Real-time Updates**: Messages are delivered instantly using SignalR.

## Contributing

If you would like to contribute to this project, please fork the repository and submit a pull request. We welcome all contributions that improve the functionality or user experience.

## Issues

If you encounter any issues or have suggestions for improvements, please open an issue on GitHub.

## License

This project is licensed under the MIT License.

## Contact

For more information, feel free to contact the project maintainers or open an issue.

---
