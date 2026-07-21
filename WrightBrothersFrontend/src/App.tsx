import { BrowserRouter as Router, Routes, Route } from "react-router-dom";

import HomePage from "./pages/HomePage"; // Import your page components
import PlaneDetail from "./pages/PlaneDetail";
import NewPlane from "./pages/NewPlane";

function App() {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<HomePage />} />
        <Route path="/planes/:planeId" element={<PlaneDetail />} />
        <Route path="/new-plane" element={<NewPlane />} />
      </Routes>
    </Router>
  );
}

export default App;
