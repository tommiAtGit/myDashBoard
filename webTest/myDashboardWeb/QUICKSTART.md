# Quick Start Guide

Get up and running with My Dashboard Web in 5 minutes!

## Option 1: Docker (Recommended - Fastest)

### Prerequisites
- Docker installed ([Get Docker](https://docs.docker.com/get-docker/))
- Docker Compose installed (included with Docker Desktop)

### Steps

1. **Navigate to the project directory**
   ```bash
   cd myDashboardWeb
   ```

2. **Build and run with Docker Compose**
   ```bash
   docker-compose up --build
   ```

3. **Access the application**
   Open your browser and go to: [http://localhost:3000](http://localhost:3000)

4. **Stop the application**
   Press `Ctrl+C` in the terminal, then run:
   ```bash
   docker-compose down
   ```

**That's it! Your dashboard is now running.**

---

## Option 2: Local Development

### Prerequisites
- Node.js 18+ or 20+ ([Download Node.js](https://nodejs.org/))
- npm (comes with Node.js)

### Steps

1. **Navigate to the project directory**
   ```bash
   cd myDashboardWeb
   ```

2. **Install dependencies**
   ```bash
   npm install
   ```
   This will take 2-3 minutes.

3. **Start the development server**
   ```bash
   npm start
   ```

4. **Access the application**
   The application will automatically open in your browser at [http://localhost:3000](http://localhost:3000)

5. **Stop the application**
   Press `Ctrl+C` in the terminal

---

## First Steps After Launch

### 1. Explore the Todo View
- Click "Todo" in the sidebar
- Click "New Task" to create your first task
- Try dragging tasks between columns (Open → In Progress → Done)

### 2. Create a Note
- Click "Notes" in the sidebar
- Click "New Document"
- Add a title, keywords, and content
- Click "Save"

### 3. Track Your Finances
- Click "Finance" in the sidebar
- View the "Current Budget" tab (default)
- See your account balances, stocks, and funds
- Switch to "Balance" tab to see assets and liabilities

---

## Common Commands

### Development
```bash
npm start           # Start development server
npm test            # Run tests
npm run build       # Build for production
```

### Docker
```bash
docker-compose up              # Start container
docker-compose up -d           # Start in background
docker-compose down            # Stop container
docker-compose logs -f         # View logs
docker-compose up --build      # Rebuild and start
```

---

## Project Structure (Quick Reference)

```
myDashboardWeb/
├── src/
│   ├── views/          # Main pages (Dashboard, Todo, Notes, Finance)
│   ├── components/     # Reusable components
│   ├── types/          # TypeScript types
│   └── utils/          # Helper functions
├── public/             # Static files
├── README.md           # Full documentation
├── ARCHITECTURE.md     # Architecture details
└── docker-compose.yml  # Docker configuration
```

---

## Troubleshooting

### Port 3000 Already in Use

**For npm:**
```bash
PORT=3001 npm start
```

**For Docker:**
Edit `docker-compose.yml` and change:
```yaml
ports:
  - "3001:80"  # Changed from 3000:80
```

### Docker Build Issues

```bash
# Clean rebuild
docker-compose down
docker system prune -a
docker-compose up --build
```

### npm Install Errors

```bash
# Clear cache and reinstall
rm -rf node_modules package-lock.json
npm install
```

---

## Next Steps

1. **Read the README.md** for full documentation
2. **Check ARCHITECTURE.md** to understand the codebase
3. **Review API.md** for data models and storage
4. **Run tests** with `npm test`
5. **Customize** the application to your needs

---

## Need Help?

- Check **README.md** for detailed documentation
- Review **ARCHITECTURE.md** for technical details
- Look at **API.md** for data structures
- Open an issue if you find a bug

---

## Key Features at a Glance

✅ **Todo Management** - Kanban board with drag & drop
✅ **Notes** - Create and organize notes with keywords
✅ **Finance Tracking** - Manage budgets, stocks, and assets
✅ **Responsive Design** - Works on desktop and mobile
✅ **Offline First** - All data stored locally
✅ **TypeScript** - Type-safe development
✅ **Docker Ready** - Easy deployment

---

**Enjoy using My Dashboard Web! 🚀**
