# Environment, Climate Change and Disaster Statistics (ECDS) Platform

A web-based GIS platform for the integration of geospatial data and information on environment, climate change, natural resources, and disasters in Bangladesh — developed under the **Strengthening Environment, Climate Change and Disaster Statistics (ECDS) Project**, Bangladesh Bureau of Statistics (BBS), Ministry of Planning.

Developed by **CEGIS** (Center for Environmental and Geographic Information Services).

---

## 📖 Background

Bangladesh is one of the most disaster-prone countries in the world due to climate change, ranking 6th on the list of countries most affected by climate disasters (1999–2018, German Watch Climate Risk Index). Sea level rise, storms, cyclones, droughts, erosion, landslides, flooding, and salinization continue to displace people and damage infrastructure and livelihoods.

To support risk-informed public investment and decision-making, BBS established the **Environment, Climate Change and Disaster Statistics (ECDS) Cell** under the Inter-Ministerial Technical Working Committee. This project delivers a comprehensive geographical interface (geo-portal) that allows government officials, academicians, and researchers to integrate disaster and climate risk data into development planning.

## 🎯 Objectives

- Empower government, academia, and researchers to securely host, manage, share, visualize, and analyze geospatial data through a sustainable geo-portal system.
- Provide operational experience in handling and publishing maps and data.
- Support government officials in project appraisal and planning processes.

Primary project outcomes include:
- Compilation of Bangladesh Environmental Statistics 2020
- Bangladesh Environmental Protection, Expenditure, Resource, and Waste Management Survey 2021
- Natural Resource: Experimental Ecosystem Accounts/Statistics in Bangladesh 2022
- Multi-sectoral GIS integration of climate/disaster-affected population, area, deaths, and missing persons

## ✨ System Features

- Spatial data analysis across different geographic regions
- Login-based authorization system with dynamic user permissions and access control
- View map and data layers organized by Theme and Components
- Download map images in PNG/JPEG format
- Upload shapefiles and data layer information
- Download shapefiles based on selected layers
- Download administrative boundary information in multiple formats
- View metadata and layer-based information based on user access level
- Administrative user management with grant/revoke access privileges
- Mobile apps available for Android (Google Play Store) and iOS (App Store)

### User Access Levels

| User Level | Dashboard | Map Viewer | Tabular Data Download | Shapefile Download | Upload File & Add Info | Update & Delete Info | Layer Info View | User Management | Access Management | Activity Log |
|---|:---:|:---:|:---:|:---:|:---:|:---:|:---:|:---:|:---:|:---:|
| BBS Admin | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ |
| BBS Registered User | ✅ | ✅ | ✅ | ✅ | ✅ | | ✅ | | | |
| Authorized User | ✅ | ✅ | ✅ | ✅ | | | ✅ | | | |
| General User | ✅ | ✅ | | | | | | | | |

## 🧩 Core Modules

### 1. Dashboard
Displays graphs, key statistics, and an interactive dynamic circular (sunburst) diagram summarizing all available themes and layers. Includes a mini map with zoom in/out, chart hover tooltips, and image-stretch zoom features. The ECDS mobile app can be downloaded directly from links on the dashboard (Play Store / App Store).

### 2. Map View Module
Available in two modes:
- **View mode** — add/remove layers, filter, change basemap, and export maps as PNG.
- **Comparison mode** — all view-mode features plus a side-by-side map comparison tool.

**Map layers (themes):**
- Administrative (Administrative Boundary, River Network, Road Network, Railway Network)
- Climate & Climate Change (Climate Change, Weather and Climate)
- Demographic and Socio-Economic (Health Hazard Data, Socio-Economic Indicators)
- Ecosystem and Biodiversity (Bio-Ecological Zone, Critical or Endangered Areas)
- Environmental & Natural Resources (Forestry, Land Use and Land Cover, Physiography and Geology, Soil and Agriculture)
- Hazard & Natural Disaster (Climate Hazard, Geophysical Hazard, Pollution and other Hazard Data)

**Customization options:** add map layers, map filtering, transparency control, background/basemap switching (Open Street, Google Hybrid, Google Satellite, Google Streets, Google Terrain, My Customer, ESRI), reset map, full-screen extent, show/hide labels, show/hide legend, and download map as PNG.

**Simple geospatial analysis toolbox:** draw polylines, polygons, rectangles, circles, markers, and circle markers; view attribute information/metadata; edit and delete drawn layers.

**Map filtering:** filter by Division, District, or Upazila; reorder/set top-most layer; adjust layer transparency.

### 3. Data View Module
Provides structured access to underlying data with copy/Excel/PDF export, column visibility toggles, search, and pagination controls.

- **Admin Boundary** — Division, District, Upazila, Union (view, add, and inline-edit records)
- **Theme** — Theme, Component, Metadata, Bundle
- **Table Information** — view/manage database table metadata
- **Theme-wise Layer Information** — add/download layers (Point, Line, Polygon, Tabular types), including shapefile-to-GeoJSON/TopoJSON conversion
- **Layer Legend Color** — add, update, and delete legend colors/icons per layer

### 4. Authorized Users
Logged-in users can view and edit their profile information (name, email, username, designation, contact number, address, date of birth, organization, profile picture) and manage account security (change password).

### 5. Administrative Modules
Restricted to privileged/admin accounts:
- **Users** — view all users, assign/revoke roles
- **User Roles** — create, edit, and delete roles (e.g., Super User, General User, System Administrator); modify granular permissions (View/Add/Edit/Delete) per component and menu item
- **Table & Column Information** — manage table and column metadata, including geo-code, map-display, and other column types
- **User Feedback** — view and search submitted contact/feedback messages, track feedback status

## 🖥️ Additional Pages

- **Contact Us** — displays ECDS contact details, embedded map/location, and a message submission form
- **About Us** — mission, vision, and a message from the Project Director

## 📱 Mobile Applications

The ECDS platform is available as a native mobile app for both major platforms:
- **Android** — available on the Google Play Store
- **iOS** — available on the Apple App Store

Mobile apps support map viewing and metadata viewing with the same layer-based navigation as the web platform.

## 🏢 Project Partners

| Organization | Role |
|---|---|
| Bangladesh Bureau of Statistics (BBS), Ministry of Planning | Project Owner |
| ECDS Cell (Inter-Ministerial Technical Working Committee) | Statistics Governance |
| CEGIS (Center for Environmental and Geographic Information Services) | Platform Development |

## 📄 License / Attribution

This repository documents the ECDS platform developed under the Government of Bangladesh's Strengthening Environment, Climate Change and Disaster Statistics (ECDS) Project, BBS. Refer to the full **User Manual (August 2022)** for detailed screenshots and step-by-step instructions on each module.

---

*For further details on any module, refer to the complete User Manual included in this repository (`user_manual.pdf` / `user_manual_admin.pdf`).*
