# EventMaster

**EventMaster** is a powerful event planning and management tool designed to help users efficiently organize conferences, workshops, and community events.

---

## Table of Contents

- [Actors](#actors)
- [Features](#features)
- [Requirements](#requirements)
- [Real-Time Functionality](#real-time-functionality)
- [Database Design](#database-design)
- [Notes](#notes)

---

## Actors

EventMaster supports three types of users:

- **Admin**
- **Event Organizer**
- **Participant**

---

## Features

### Authentication

- All users can log in and log out.
- Participants can view events without logging in, but must log in to interact further.

### Admin Capabilities

- Approve or reject newly registered Event Organizer accounts.
- Approve or reject events submitted by Event Organizers. Approved events become visible to all Participants.

### Participant Capabilities

- View all approved and posted events.
- Search for events by **location**, **date**, and other criteria.
- Register and pay for tickets (if available).
- Save important events.
- Receive real-time notifications and updates.
- Download event-related attachments.

### Event Organizer Capabilities

- Full CRUD access: **Create**, **Read**, **Update**, **Delete** events.
- Upload event-related materials (e.g., PDFs, images).
- Send updates and notifications to registered participants.

---

## Real-Time Functionality

- **Socket-based communication** for:
  - Notifications
  - Real-time event updates

---

## Database Design

- A full **DB schema**.
- Include all key entities:
  - Users (Admin, Organizer, Participant)
  - Events
  - Tickets
  - Attachments
  - Notifications
  - Payments
