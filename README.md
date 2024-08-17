# BiteBlogs

BiteBlogs is an ASP.NET Core MVC application designed to provide a platform for food-related blogs. Users can view, like, and comment on blogs, while admins have advanced functionality to manage blogs, users, and other resources.

## Current Status

- **In Progress:**
  - Implementing the like functionality.
  - Enhancing the user interface and user experience.


## Features

- **User Interactions:**
  - View food-related blogs.
  - Like and comment on blogs (if signed in).
- **User Registration,Login,Logout**
   -User can register
   -user can login and logout  
- **Admin Capabilities:**
  - Add, edit, delete, and update blogs.
  - CRUD operations on users, tags, and other entities.
- **Image Management:**
  - Integrates with Cloudinary for image uploads and management.
- **Pagination, Sorting, and Filtering:**
  - Efficiently handle large sets of data.
- **Enhanced Blog Writing:**
  - Utilizes the Floara text editor for rich text formatting.


## Technologies Used

- **ASP.NET Core MVC**: Framework for building the web application.
- **Entity Framework Core**: ORM for database access with SQL Server.
- **Identity Framework**: For user authentication and authorization.
- **Repository Pattern**: To manage data access and promote separation of concerns.
- **Cloudinary**: For handling and serving images.
- **Floara Text Editor**: To enhance blog writing capabilities.



## Setup and Installation

1. **Clone the Repository:**
   ```bash
   git clone https://github.com/YOUR_USERNAME/BitBlogsProjectRepository.git

 2. **nevigate to project Directory:**

 3. **Install Dependencies:**
    Make sure you have the .NET SDK installed. Then, restore the project dependencies:
    dotnet restore

  4. **Configure Database:**
    Update the appsettings.json file with your SQL Server connection string.

  5. **Run Migrations:**
    Apply database migrations:

    bash
    dotnet ef database update
    Run the Application:

    bash
    dotnet run
    Access the Application:
    Open a web browser and navigate to https://localhost:5001 or http://localhost:5000.

 6. **Contributing**
    Feel free to fork the repository, make changes, and submit pull requests. Ensure that you follow the coding standards and include tests where applicable.

7. **License**
    This project is licensed under the MIT License.

**Contact**
For any questions or feedback, please contact 
 -  abhinandandesai.professional@gmail.com
 -  AbhinandanVDesai







