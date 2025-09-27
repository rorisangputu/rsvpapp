# Be There — RSVP Web App

**Be There** is a web application that allows users to create, manage, and share events with friends. Attendees can easily RSVP, giving notice of their attendance and staying up-to-date on event details. The platform is designed to simplify event coordination and enhance social planning.

The application is built with **C# ASP.NET Web API** for the backend and **React** for the frontend, providing a modern, full-stack experience.

**In Development**
---

## Features

- **Event Creation & Management:** Users can create events, set details (date, time, location), and manage them dynamically.
- **RSVP Functionality:** Attendees can respond to invitations, with statuses such as attending, not attending, or maybe.
- **User Profiles:** Manage personal information and view events you are attending or hosting.
- **Event Sharing:** Easily share events with friends via links or email invitations.
- **Real-Time Updates:** Event status and attendee lists are updated in real-time to keep everyone informed.
- **Responsive UI:** Optimized for both desktop and mobile devices for a seamless user experience.

---

## Tech Stack

- **Frontend:** React, JavaScript, HTML, CSS  
- **Backend:** C# ASP.NET Core Web API  
- **Database:** MySQL (via Entity Framework or chosen ORM)  
- **Authentication & Security:** JWT-based authentication for secure user access  

---

## Architecture & Design

- **API-Driven Backend:** RESTful endpoints built with ASP.NET Core Web API for robust CRUD operations.  
- **Frontend-Backend Integration:** React communicates with the API via HTTP requests, maintaining a dynamic and responsive interface.  
- **Modular & Scalable:** Codebase structured for maintainability and future feature expansion.  
- **Data Management:** Users, events, and RSVPs are securely stored and managed in a relational database.  

---

## Future Enhancements
 
- Email reminders and RSVP confirmations  
- Event categories and filters for easier discovery  
- Admin dashboard for managing users and events  
