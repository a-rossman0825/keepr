# 📌 Keepr

> A Pinterest-inspired social media platform for discovering, creating, and organizing visual content

![Keepr Logo](client/src/assets/img/keepr_logo.png)

## 🌟 Overview

Keepr is a modern, full-stack web application that allows users to discover, save, and organize visual content in a Pinterest-like interface. Users can create "keeps" (image posts with descriptions), organize them into private or public "vaults" (collections), and explore content from other users in a responsive masonry layout.

**Live Demo:** [Coming Soon]

## ✨ Key Features

### 🎨 Content Management

- **Create Keeps**: Upload images with names and descriptions
- **Organize Vaults**: Create collections to categorize your keeps
- **Privacy Controls**: Toggle vaults between private and public visibility
- **View Tracking**: Automatic view count increment for keeps

### 👥 Social Features

- **User Profiles**: View other users' public keeps and vaults
- **Content Discovery**: Browse all public keeps in a masonry feed
- **Creator Attribution**: Each keep displays its creator's profile

### 📱 User Experience

- **Responsive Design**: Optimized for desktop, tablet, and mobile
- **Masonry Layout**: Pinterest-style responsive grid layout
- **Modal Interfaces**: Smooth modal experiences for detailed views
- **Real-time Updates**: Live content updates without page refresh

### 🔐 Authentication & Security

- **Auth0 Integration**: Secure authentication and user management
- **JWT Authorization**: Protected API endpoints
- **User-based Permissions**: Users can only edit/delete their own content

## 🛠️ Tech Stack

### Frontend

- **Vue.js 3** - Progressive JavaScript framework with Composition API
- **Vue Router 4** - Client-side routing
- **Vite** - Fast build tool and development server
- **Bootstrap 5** - CSS framework for responsive design
- **Sass** - CSS preprocessor for enhanced styling
- **Axios** - HTTP client for API communication
- **Socket.io Client** - Real-time communication

### Backend

- **.NET 9** - Modern web API framework
- **ASP.NET Core** - Web application framework
- **Dapper** - Lightweight ORM for database operations
- **MySQL** - Relational database
- **Auth0** - Authentication and authorization service
- **Swagger** - API documentation

### Development Tools

- **ESLint** - JavaScript linting
- **Visual Studio Code** - Development environment
- **Git** - Version control

## 🏗️ Architecture

### Database Design

```sql
Tables:
├── accounts (User profiles with Auth0 integration)
├── keeps (Image posts with metadata)
├── vaults (Collections/folders for organizing keeps)
└── vault_keep (Many-to-many relationship between vaults and keeps)
```

### API Structure

```
REST API Endpoints:
├── /api/keeps - CRUD operations for keeps
├── /api/vaults - CRUD operations for vaults
├── /api/vaultkeeps - Managing keeps within vaults
├── /api/profiles - User profile operations
└── /api/account - Account management
```

### Frontend Architecture

```
Vue.js Structure:
├── Components (Reusable UI components)
├── Pages (Route-based views)
├── Services (API communication layer)
├── Models (Data models with validation)
├── Utils (Helper functions and utilities)
└── Assets (Images, styles, and static content)
```

## 🚀 Getting Started

### Prerequisites

- **Node.js** (v16 or higher)
- **.NET 9 SDK**
- **MySQL Server**
- **Auth0 Account** (for authentication setup)

### Installation

1. **Clone the repository**

   ```bash
   git clone https://github.com/a-rossman0825/keepr.git
   cd keepr
   ```

2. **Setup Backend**

   ```bash
   cd server

   # Install dependencies
   dotnet restore

   # Create database (update connection string in appsettings.json)
   mysql -u root -p < dbSetup.sql

   # Configure environment variables
   cp appsettings.example.json appsettings.Development.json
   # Update with your Auth0 and database credentials

   # Run the server
   dotnet run
   ```

3. **Setup Frontend**

   ```bash
   cd client

   # Install dependencies
   npm install

   # Configure environment (update src/env.js with your API URL)

   # Start development server
   npm run dev
   ```

4. **Access the application**
   - Frontend: `http://localhost:8080`
   - Backend API: `https://localhost:7045`
   - API Documentation: `https://localhost:7045/swagger`

## 📁 Project Structure

```
keepr/
├── client/                    # Vue.js frontend application
│   ├── src/
│   │   ├── components/       # Reusable Vue components
│   │   ├── pages/           # Route-based page components
│   │   ├── services/        # API service layer
│   │   ├── models/          # Data models
│   │   ├── utils/           # Helper utilities
│   │   └── assets/          # Static assets
│   ├── public/              # Public assets
│   └── package.json         # Frontend dependencies
│
├── server/                   # .NET Web API backend
│   ├── Controllers/         # API route controllers
│   ├── Services/           # Business logic layer
│   ├── Repositories/       # Data access layer
│   ├── Models/             # Data models
│   ├── Interfaces/         # Service contracts
│   └── keepr.csproj        # Backend dependencies
│
├── README.md               # This file
└── keepr.sln              # Visual Studio solution file
```

## 🎯 Key Learning Outcomes

This project demonstrates proficiency in:

- **Full-Stack Development**: End-to-end application development
- **Modern Web Technologies**: Vue 3, .NET 9, and contemporary tooling
- **RESTful API Design**: Well-structured, documented API endpoints
- **Database Design**: Normalized relational database with proper relationships
- **Authentication**: Secure user authentication and authorization
- **Responsive Design**: Mobile-first, cross-device compatibility
- **Version Control**: Git workflow and project organization

## 🔄 Development Workflow

```bash
# Development commands
npm run dev          # Start frontend development server
dotnet run           # Start backend API server
npm run build        # Build frontend for production
dotnet publish       # Publish backend for deployment
```

## 🤝 Contributing

This is a portfolio project, but feedback and suggestions are welcome! Please feel free to:

1. Fork the repository
2. Create a feature branch
3. Submit a pull request

## 📄 License

This project is open source and available under the [MIT License](LICENSE).

## 👨‍💻 Developer

**Alex Rossman**

- GitHub: [@a-rossman0825](https://github.com/a-rossman0825)
- LinkedIn: [Connect with me](https://linkedin.com/in/alex-rossman)
- Portfolio: [View my work](https://alex-rossman.dev)

---

_Built with ❤️ as part of the CodeWorks Full-Stack Development Program_
