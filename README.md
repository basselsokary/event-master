# EventMaster

**EventMaster** is a modular, scalable event management system designed using **Clean Architecture** and **Domain-Driven Design (DDD)** principles. It's designed to help users efficiently organize conferences, workshops, and community events.

---

## Table of Contents

- [System Architecture](#system-architecture)
- [Actors](#actors)
- [Features](#features)
  - [Authentication & Authorization](#authentication--authorization)
  - [Admin Capabilities](#admin-capabilities)
  - [Participant Capabilities](#participant-capabilities)
  - [Event Organizer Capabilities](#event-organizer-capabilities)
- [Real-Time Communication](#real-time-communication)
- [Database Design](#database-design)
- [Technology Stack](#technology-stack)
<!-- - [Notes](#notes) -->

---

## System Architecture

EventMaster is structured according to Clean Architecture and DDD practices:

- **Domain Layer**: Contains core business logic and aggregates (e.g., Event, Basket).
- **Application Layer**: Coordinates use cases, CQRS-based handlers, and validation.
- **Infrastructure Layer**: Handles EF Core, database, identity, and external services.
- **API Layer**: Exposes HTTP endpoints and handles request/response formatting.

---

## Actors

- **Admin**: Oversees platform activity and content approval.
- **Event Organizer**: Creates, manages, and tracks events.
- **Participant**: Browses, registers for, and interacts with events.

---

## Features

### Authentication & Authorization

- Secure login/sign-up for all users.
- Anonymous browsing for participants.
- Role-based access control using Identity.

---

### Admin Capabilities

- Review and approve/reject new Event Organizer accounts.
- Review and approve/reject submitted events before public display.

---

### Participant Capabilities

- View approved/public events.
- Filter/search events by **location**, **date**, and other criteria.
- Register and pay for tickets (if available).
- Save favorite events for quick access.
- Get real-time notifications and event updates.
- Download event attachments (e.g., PDF flyers).

---

### Event Organizer Capabilities

- Full CRUD access: **Create**, **Read**, **Update**, **Delete** events.
- Upload event-related assets (images, documents).
- Send updates and notifications to registered participants.
<!-- - View participant registration statistics. -->

---

## Real-Time Communication

Built using **SignalR** for efficient real-time features:

- Live notifications (admin actions, event updates).
- Real-time event updates.
- Organizer-to-participant announcements.

---

## Database Design

The system includes a relational schema designed around DDD aggregates and normalized entities:

- **Users**: Admins, Organizers, Participants.
- **Events**: Core domain aggregate.
- **Tickets**: Linked to participants and events.
- **Attachments**: Linked to events.
<!-- - **Payments**: Secure transaction records. -->
- **Baskets**: Participant-level saved items.
- **SavedEventItems**: Join table linking Baskets and Events.
- **Notifications**: Real-time and system messages.

---

## Technology Stack

- **Backend**: ASP.NET Core Web API (.NET 8)
- **Database**: Entity Framework Core (Code First) with SQL Server.
- **Authentication**: ASP.NET Identity + JWT
- **Real-time**: SignalR
- **Architecture**: Clean Architecture + Domain-Driven Design
- **Validation**: FluentValidation
- **Data Access**: CQRS, Repository with Unit of Work Pattern

<!-- ---

## Notes

- Designed for extensibility: modular, testable, and future-proof.
- Fully decoupled architecture ensures flexibility in swapping implementations.
- Supports future enhancements like event feedback, user analytics, or email notifications. -->

