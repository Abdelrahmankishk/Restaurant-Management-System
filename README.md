# 🍽️ Restaurant Management System

A **C# console-based Restaurant Management System** built as an Object-Oriented Programming (OOP) project. The application models the operations of a multi-branch restaurant chain, applying object-oriented design principles to organize restaurant operations and enforce business rules.

The system provides a menu-driven interface for managing branches, employees, customers, menu items, orders, ingredient inventory, deliveries, customer feedback, and operational reports.

## ✨ Project Highlights

- 🏢 Manage multiple restaurant branches and their operations
- 👥 Apply inheritance and polymorphism through specialized employee roles
- 📋 Manage menu items, add-ons, ingredients, and recipes
- 🛒 Process dine-in, takeaway, and delivery orders
- 📦 Track branch-specific ingredient inventory and stock consumption
- 🚚 Manage delivery assignments and delivery status
- ⭐ Collect customer feedback and ratings
- 📊 Generate operational reports and summaries
- 🔐 Enforce role-based permissions and business rules
- 🧩 Apply object-oriented design and separation of responsibilities
- 📐 Model the system using a UML class diagram

## 🧭 System Overview

The application provides nine main functional modules through its console interface.

| **Option** | **Module** | **Description** |
|---|---|---|
| `1` | 🏢 Branch Management | Manage branches, view branch details, and configure menu availability. |
| `2` | 👥 Employee Management | Manage employees, branch assignments, delivery staff, and shift schedules. |
| `3` | 🧑 Customer Management | Register customers, view customer information, and inspect order history. |
| `4` | 🍔 Menu Management | Manage menu items, add-ons, ingredients, and recipes. |
| `5` | 🛒 Order Management | Place orders, manage preparation, process payments, and handle cancellations. |
| `6` | 🚚 Delivery Management | Assign delivery staff and track delivery outcomes. |
| `7` | 📦 Inventory Management | View ingredient stock, restock inventory, and inspect ingredients. |
| `8` | ⭐ Feedback | Submit customer feedback and review existing feedback. |
| `9` | 📊 Reports & Analytics | Review revenue, order statuses, popular menu items, loyalty points, inventory, and ratings. |
| `0` | 🚪 Exit | Close the application. |

## 🏗️ Object-Oriented Design

The system uses object-oriented concepts to represent the restaurant domain and organize application behavior.

### 🧬 Inheritance

The abstract `Employee` class defines common employee information and behavior. Specialized classes inherit from it:

- `Chef`
- `Waiter`
- `Cashier`
- `BranchManager`

This reduces duplication and allows the system to work with different employee roles through a common abstraction.

### 🎭 Abstraction

The `Employee` class represents the shared concept of a restaurant employee without allowing a generic employee instance to be created directly.

Role-specific classes provide their own specialized behavior through the common employee contract.

### 🔄 Polymorphism

The application uses different employee types through the common `Employee` abstraction. Role-specific behavior is used to distinguish permissions and responsibilities, such as placing orders, preparing food, processing payments, and managing branches.

### 🔒 Encapsulation and Business Rules

Business operations are coordinated through service classes. These services validate requests and control state changes, helping prevent operations that violate the defined workflows.

Examples include checking whether an employee is eligible to perform an operation, verifying menu availability, validating inventory requirements, and restricting feedback to completed orders.

### 🧩 Separation of Responsibilities

The application separates domain models, business operations, console presentation, and supporting utilities into dedicated folders. This makes the code easier to navigate, maintain, and extend.

## 🏛️ Main Domain Model

The domain model represents the entities and relationships required to operate a restaurant chain.

| **Class / Model** | **Responsibility** |
|---|---|
| `Branch` | Represents a restaurant branch and its contact information, opening hours, and manager. |
| `Employee` | Abstract base class for common employee information and behavior. |
| `Chef`, `Waiter`, `Cashier`, `BranchManager` | Represent specialized employee roles and responsibilities. |
| `ShiftSchedule` | Records employee shift assignments and scheduled time slots. |
| `Customer` | Stores customer information and loyalty points. |
| `MenuItem` | Represents a menu item, including its name, description, category, and base price. |
| `AddOn` | Represents optional additions and their extra prices. |
| `BranchMenuItem` | Associates menu items with branches and their availability and price overrides. |
| `Order` | Represents a customer order, including its type, branch, payment details, and status. |
| `OrderItem` | Stores the menu items purchased, quantities, prices, notes, and selected add-ons. |
| `Ingredient` | Represents an ingredient used in food preparation. |
| `BranchInventory` | Tracks ingredient quantities separately for each branch. |
| `RecipeItem` | Defines the required quantity of an ingredient for a menu item. |
| `DeliveryStaff` | Stores delivery personnel details and availability. |
| `Delivery` | Tracks delivery assignments, status, time, and failure information. |
| `Feedback` | Stores customer ratings and comments for completed orders. |

## 📜 Key Business Rules

The application incorporates business rules to keep restaurant workflows consistent.

### 🛒 Order Management

- Only **Waiters and Cashiers** can place orders.
- The employee placing an order must be assigned to the same branch.
- An order must contain at least one item, and item quantities must be greater than zero.
- Menu items must be available at the selected branch.
- Branch-specific prices override the default menu item price when configured.
- Delivery orders require a delivery address and an associated delivery record.

### 👨‍🍳 Order Workflow

Orders follow a controlled workflow:

`Pending` → `Preparing` → `Served` → `Completed`

- A Chef assigned to the branch manages preparation and serving transitions.
- A Cashier processes payment for a served order.
- Eligible employees can cancel orders according to their roles and the order's current status.
- A Branch Manager can authorize the documented inventory override when stock is insufficient, subject to the same-branch restriction.

### 📦 Inventory Management

- Each branch maintains its own ingredient stock.
- Recipes define ingredient quantities required per serving.
- The system calculates ingredient requirements based on the items and quantities in an order.
- Preparation checks stock availability and deducts the required quantities.
- Ingredient stock can be replenished through the inventory module.

### 🚚 Delivery Management

- Delivery staff can be assigned to eligible delivery orders after the order reaches `Served`.
- Only authorized employees can assign delivery staff.
- Assigned staff must be available and belong to the same branch as the order.
- Only the assigned staff member can update the delivery outcome.
- Successful deliveries complete the associated order.
- Failed deliveries require a failure reason and result in order cancellation.

### ⭐ Feedback and Loyalty

- Feedback is accepted only for completed orders.
- Customers can submit feedback only for their own orders.
- A customer can submit only one feedback entry per order.
- Ratings must be between **1 and 5 stars**; comments are optional.
- Loyalty points are calculated using the integer floor of the order total when an order is completed.

## 🗂️ Project Structure

The project follows a single-project architecture with responsibilities separated by folder.

```text
Restaurant-Management-System/
│
├── Restaurant Management System.sln
│
└── Restaurant Management System/
    │
    ├── Helpers/
    │   ├── ConsoleHelper.cs
    │   └── DataSeeder.cs
    │
    ├── Models/
    │   ├── Enums/
    │   │   ├── DeliveryStatus.cs
    │   │   ├── OrderStatus.cs
    │   │   ├── OrderType.cs
    │   │   ├── PaymentMethod.cs
    │   │   └── UnitTypes.cs
    │   │
    │   ├── AddOn.cs
    │   ├── Branch.cs
    │   ├── BranchInventory.cs
    │   ├── BranchManager.cs
    │   ├── BranchMenuItem.cs
    │   ├── Cashier.cs
    │   ├── Chef.cs
    │   ├── Customer.cs
    │   ├── Delivery.cs
    │   ├── DeliveryStaff.cs
    │   ├── Employee.cs
    │   ├── Feedback.cs
    │   ├── Ingredient.cs
    │   ├── MenuItem.cs
    │   ├── Order.cs
    │   ├── OrderItem.cs
    │   ├── RecipeItem.cs
    │   ├── ShiftSchedule.cs
    │   └── Waiter.cs
    │
    ├── Services/
    │   ├── DeliveryService.cs
    │   ├── FeedBackService.cs
    │   ├── InventoryServices.cs
    │   └── OrderServices.cs
    │
    ├── UI/
    │   ├── BranchUI.cs
    │   ├── CustomerUI.cs
    │   ├── DeliveryUI.cs
    │   ├── EmployeeUI.cs
    │   ├── FeedbackUI.cs
    │   ├── InventoryUI.cs
    │   ├── MainMenu.cs
    │   ├── MenuUI.cs
    │   ├── OrderUI.cs
    │   └── ReportsUI.cs
    │
    ├── Program.cs
    └── Restaurant Management System.csproj
```

### 📁 Folder Responsibilities

| **Folder** | **Responsibility** |
|---|---|
| `Models` | Contains domain classes, specialized employee types, and enumerations. |
| `Services` | Coordinates business operations for orders, inventory, deliveries, and feedback. |
| `UI` | Contains menu-driven console interfaces for each system module. |
| `Helpers` | Provides console utilities and initializes sample application data. |

## 🔄 Application Flow

```text
             Program.cs
                 │
                 ▼
             DataSeeder
                 │
                 ▼
              MainMenu
                 │
       ┌─────────┼─────────┐
       ▼         ▼         ▼
     Models    Services     UI
       │         │         │
       └─────────┼─────────┘
                 ▼
        Restaurant Operations
```

`Program.cs` configures the console culture, initializes sample data through `DataSeeder`, and launches `MainMenu`. The UI modules collect user input and invoke the relevant services to perform operations.

## 🛠️ Technologies

- **C#**
- **.NET 8**
- **Object-Oriented Programming (OOP)**
- **UML Class Diagram**
- **Console Application**

## 🚀 Future Enhancements

- Convert the application to **Entity Framework Core (EF Core)** and connect the entities to a **SQL Server database** for persistent data storage.

## 👨‍💻 Author

**Abdelrahman Kishk**

- GitHub: [@Abdelrahmankishk](https://github.com/Abdelrahmankishk)