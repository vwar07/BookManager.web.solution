Book Manager Application

Overview
The Book Manager Application is a comprehensive solution for managing books, authors, publishers, and user carts. It provides functionalities such as adding books, browsing book details, managing a shopping cart, placing orders.


Getting Started

	Prerequisites

		Development Environment:

			Visual Studio 2022 or later
			.NET 8.0 SDK
			SQL Server (LocalDB or full version)

		Packages:

			EntityFrameworkCore
			EntityFrameworkCore.Tools
			EntityFrameworkCore.SqlServer
			Microsoft.AspNetCore.Identity
			Bootstrap and jQuery for frontend

Update Connection String:

	Open the appsettings.json file in the root directory.
	Update the "BookManagerConnectionString" string to point to your local SQL Server instance.



How to Use
	Admin Panel
		Login with the Admin credentials. username - vigneshwaranravi07@gmail.com, password - Vignesh@98
		
	Navigate to:
		Books: Add new books.

	User Features
		Register and Login:
			Users can register and log in to their accounts.
		Browse Books:
			Use the search bar or sorting dropdown to find books by criteria.
		Manage Cart:
			Add books to the cart, update quantities, or remove items.
		Place Orders:
			Confirm order details and place orders.
		View Orders:
			Navigate to the "Orders" section to view all placed orders.

Technologies Used
	Frontend: Bootstrap, jQuery, Razor Views
	Backend: ASP.NET Core MVC
	Database: SQL Server with Entity Framework Core
	Authentication: ASP.NET Core Identity
	Deployment: IIS or Kestrel Server



API Endpoints Test
		This application includes a Swagger UI for testing the APIs. You can access the Swagger documentation and testing interface at:

Swagger URL: /swagger/index.html
		There are two endpoints for retrieving books, offering the same functionality but implemented differently:

1. Entity Framework Endpoint: /Books
2. Stored Procedure Endpoint: /books-with-filters

Parameter	Description
search		Used to search for books by title, author (first and last name), or publisher name.
sortBy		Determines the sorting criteria. Available values: default, title, priceLowHigh, priceHighLow, publisherAuthorTitle, authorTitle.
pageNumber	Specifies the page number for pagination.
pageSize	Specifies the number of items per page for pagination.



	
