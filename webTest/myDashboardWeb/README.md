# My Dashboard Web Application

A comprehensive web-based dashboard application built with React, TypeScript, and Material-UI. This application provides task management (Todo), note-taking, and personal finance tracking capabilities.

## Features

### 📋 Todo Management
- **Kanban Board**: Visual task management with three swim lanes (Open, In Progress, Done)
- **Drag & Drop**: Move tasks between columns by dragging them
- **Task CRUD**: Create, read, update, and delete tasks
- **Task Details**: Each task includes title, description, owner, and status
- **Persistent Storage**: All tasks are saved to local storage

### 📝 Notes
- **Note Management**: Create and manage notes with keywords and summaries
- **Search & Filter**: Search notes by title, keywords, or date
- **Recall Feature**: AI-powered recall functionality (UI ready for integration)
- **Rich Text Support**: Store detailed notes with structured content
- **Persistent Storage**: All notes are saved to local storage

### 💰 Finance Management

#### Current Budget Tab
- **Account Balances**: Track current and savings account balances
- **Stock Portfolio**: Manage stocks with real-time value calculations
  - Stock name, price, quantity
  - Automatic current value calculation
- **Fund Tracking**: Monitor investment funds
- **Visual Summary**: Card-based display of account totals

#### Balance Tab
- **Assets Management**: Track all assets with values
- **Liabilities Management**: Monitor debts and liabilities
- **Net Balance Calculation**: Automatic calculation of total balance (Assets - Liabilities)
- **Editable Tables**: Inline editing of all financial data

### 🎨 UI/UX Features
- **Responsive Design**: Works on desktop, tablet, and mobile devices
- **Modern Interface**: Clean, professional design with Material-UI components
- **Font Awesome Icons**: Rich iconography throughout the application
- **Collapsible Sidebar**: Toggle navigation sidebar for more screen space
- **Dark Theme Elements**: Professional color scheme optimized for readability

## Technology Stack

- **React 18.2.0**: Modern React with hooks and functional components
- **TypeScript 5.3.3**: Type-safe development
- **Material-UI 5.15.7**: Component library for consistent UI
- **React Beautiful DnD 13.1.1**: Drag and drop functionality
- **React Router 6.21.3**: Client-side routing
- **Font Awesome 6.5.1**: Icon library
- **Docker**: Containerization for easy deployment

## Project Structure

```
myDashboardWeb/
├── public/                      # Static files
│   ├── index.html              # HTML template
│   └── manifest.json           # PWA manifest
├── src/
│   ├── components/             # Reusable components
│   │   ├── Header.tsx         # Application header
│   │   ├── Sidebar.tsx        # Navigation sidebar
│   │   └── TaskModal.tsx      # Task creation/edit modal
│   ├── views/                  # Page components
│   │   ├── Dashboard.tsx      # Home dashboard
│   │   ├── Todo.tsx           # Todo management
│   │   ├── Notes.tsx          # Notes management
│   │   └── Finance.tsx        # Finance tracking
│   ├── types/                  # TypeScript type definitions
│   │   └── index.ts           # All type definitions
│   ├── utils/                  # Utility functions
│   │   └── helpers.ts         # Helper functions and storage
│   ├── __tests__/             # Test files
│   │   ├── App.test.tsx
│   │   ├── Dashboard.test.tsx
│   │   ├── Todo.test.tsx
│   │   ├── Notes.test.tsx
│   │   ├── Finance.test.tsx
│   │   └── helpers.test.ts
│   ├── App.tsx                # Main application component
│   ├── index.tsx              # Application entry point
│   ├── styles.css             # Global styles
│   ├── setupTests.ts          # Test configuration
│   └── react-app-env.d.ts     # React types
├── docker-compose.yml          # Docker Compose configuration
├── Dockerfile                  # Docker build instructions
├── nginx.conf                  # Nginx configuration
├── package.json                # Dependencies and scripts
├── tsconfig.json              # TypeScript configuration
└── README.md                   # This file
```

## Getting Started

### Prerequisites

- Node.js 18+ or 20+
- npm or yarn
- Docker and Docker Compose (for containerized deployment)

### Local Development

1. **Install Dependencies**
   ```bash
   npm install
   ```

2. **Start Development Server**
   ```bash
   npm start
   ```
   The application will open at [http://localhost:3000](http://localhost:3000)

3. **Run Tests**
   ```bash
   npm test
   ```

4. **Run Tests with Coverage**
   ```bash
   npm run test:coverage
   ```

5. **Build for Production**
   ```bash
   npm run build
   ```
   Creates an optimized production build in the `build` folder.

### Docker Deployment

1. **Build and Run with Docker Compose**
   ```bash
   docker-compose up --build
   ```
   The application will be available at [http://localhost:3000](http://localhost:3000)

2. **Run in Background**
   ```bash
   docker-compose up -d
   ```

3. **Stop the Container**
   ```bash
   docker-compose down
   ```

4. **View Logs**
   ```bash
   docker-compose logs -f
   ```

### Docker Manual Build

If you prefer to build and run Docker manually:

```bash
# Build the image
docker build -t my-dashboard-web .

# Run the container
docker run -p 3000:80 my-dashboard-web
```

## Usage Guide

### Todo View

1. **Create a Task**
   - Click "New Task" button
   - Fill in task details (title, description, owner, status)
   - Click "Save"

2. **Move Tasks**
   - Drag and drop tasks between columns
   - Status automatically updates based on column

3. **Edit/Delete Tasks**
   - Click the pencil icon to edit
   - Click the trash icon to delete

### Notes View

1. **Create a Note**
   - Click "New Document"
   - Enter title, keywords, content, and summary
   - Click "Save"

2. **Search Notes**
   - Use the search bar to filter notes
   - Select filter type (title, keywords, or date)

3. **Edit Notes**
   - Click on a note to view it
   - Click the pencil icon to edit
   - Make changes and click "Save"

### Finance View

#### Current Budget Tab

1. **Update Accounts**
   - Edit current and savings account balances directly in the table

2. **Manage Stocks**
   - Click "Add Stock" to add new entries
   - Edit stock name, price, and quantity
   - Current value is calculated automatically
   - Click trash icon to remove

3. **Manage Funds**
   - Click "Add Fund" to add new entries
   - Edit fund name and price
   - Click trash icon to remove

#### Balance Tab

1. **Track Assets**
   - Click "Add Asset" to add new entries
   - Edit asset name and value
   - Click trash icon to remove

2. **Track Liabilities**
   - Click "Add Liability" to add new entries
   - Edit liability name and value
   - Click trash icon to remove

3. **View Balance**
   - Current balance is calculated automatically
   - Shows total assets and liabilities

## Data Persistence

All data is stored in the browser's local storage:
- Tasks persist between sessions
- Notes persist between sessions
- Financial data persists between sessions
- Data is specific to each browser/device

## Testing

The application includes comprehensive test suites:

- **Unit Tests**: Testing individual components and functions
- **Integration Tests**: Testing component interactions
- **Coverage**: Aim for >80% code coverage

Run tests with:
```bash
npm test                    # Interactive mode
npm run test:coverage       # With coverage report
```

## Customization

### Styling

All styles are centralized in `src/styles.css`. You can customize:
- Color scheme (CSS variables at the top)
- Typography
- Layout dimensions
- Component styles

### Adding Features

1. **New Views**: Add new view components in `src/views/`
2. **New Components**: Add reusable components in `src/components/`
3. **New Routes**: Update routing in `src/App.tsx`
4. **New Types**: Add TypeScript types in `src/types/index.ts`

## Browser Support

- Chrome (latest)
- Firefox (latest)
- Safari (latest)
- Edge (latest)

## Performance

- Code splitting with React.lazy (ready for implementation)
- Optimized production build
- Gzip compression in Nginx
- Asset caching

## Security Considerations

- No external API calls (fully client-side)
- Local storage only (no server-side data)
- HTTPS recommended for production
- Content Security Policy headers recommended

## Troubleshooting

### Port Already in Use

If port 3000 is already in use:
```bash
# For development
PORT=3001 npm start

# For Docker
# Edit docker-compose.yml and change "3000:80" to "3001:80"
```

### Build Errors

```bash
# Clear cache and reinstall
rm -rf node_modules package-lock.json
npm install
```

### Docker Issues

```bash
# Remove all containers and rebuild
docker-compose down
docker-compose up --build --force-recreate
```

## Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## License

This project is licensed under the MIT License.

## Support

For issues, questions, or contributions, please open an issue in the repository.

## Roadmap

Future enhancements:
- [ ] Backend API integration
- [ ] User authentication
- [ ] Data synchronization across devices
- [ ] Export/import functionality
- [ ] Advanced reporting and analytics
- [ ] Dark mode toggle
- [ ] Multi-language support
- [ ] Calendar integration
- [ ] File attachments for notes
- [ ] Budget forecasting tools

## Acknowledgments

- Material-UI for the component library
- React Beautiful DnD for drag and drop
- Font Awesome for icons
- Create React App for the initial setup
