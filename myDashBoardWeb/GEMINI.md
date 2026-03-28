# GEMINI.md - myDashBoardWeb Guidelines

## UI Stack
- **Framework:** React 18.
- **Styling:** Material UI (MUI) version 6.
- **Routing:** React Router DOM version 6.
- **API Communication:** Axios.
- **Icons:** React Icons and MUI Icons.

## Architectural Patterns
- **Components:** Functional components with Hooks.
- **State Management:** Local state (`useState`, `useContext`) as appropriate for a prototype.
- **Services:** Separate API calls into service functions or custom hooks.

## Coding Standards
- **Naming:** PascalCase for components, camelCase for functions and variables.
- **TypeScript:** Use TypeScript where possible (verify if currently using JS or TS in `src`).
- **Styles:** Use MUI's `sx` prop or styled components for custom styling.

## Development Workflow
- **Node.js:** Requires `NODE_OPTIONS=--openssl-legacy-provider` to run `npm start` due to CRA/Webpack constraints.
- **Testing:** Use Jest and React Testing Library for component testing.
