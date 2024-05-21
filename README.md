# TA'AM

## Introduction

- This is an E-commerce application built using ASP.Net Core API with a clean architecture approach
- TA'Am is designed to manage the process of selling and buying used clothes between users by Enhancing the user experience by analyzing and optimizing an image captured by  the user to showcase their products using machine learning models and applying real-time features like chatting and uploading the posts.

## Features

- User Authentication and Authorization
- Chat, Email, Post, and User Management "CRUD Operations"
- Clean Architecture
- Unit Testing
- Real-Time

## Technologies Used

- ASP.Net Core
- Entity Framework Core
- SSMS
- AutoMapper
- FluentValidation
- MediatR
- CQRS
- Generic Design Pattern
- Custom Routing
- Response and Response Handler
- Custom Controller with response
- Dependency Injection
- Localization
- Middlewares
- Pagination
- Data Seeding

## Project Structure

- Data Layer (Domain Layer)
	- It includes the enterprise logic like their specifications and the entities
	- It stands at the center of the application where the application entities exist
- Infrastructure Layer
	- It includes the model objects which maintain the entire database context objects and the database migrations in this layer 
	- It has the responsibilities of entire domain model objects
	- References Data Layers
- Service Layer
	- It includes the application-specific business logic
	- The service layer often contains logic for authentication, authorization, validation, and other application-specific operations
	- References Infrastructure, and Data Layers
- Core Layer
	- It is responsible for handling the requests from the presentation layer using abstractions or interfaces
	- References Service, Infrastructure, and Data Layers
- Presentation Layer
	- This layer includes UI components such as controllers, views, and view models. In ASP.NET Core, this typically involves MVC, API, or Razor pages 
	- References Core Layer

## Current Work and Future Plans

### Currently Working On

  - Enhancing and modifying various endpoints for improved functionality
  - Implementing unit tests to ensure code reliability
  - Adding real-time features for a more dynamic user experience
    
### Future Work

  - Integrating with machine learning models to enhance user experience 
