import { BrowserRouter } from "react-router-dom";
import "./App.css";
import Navbar from "./components/Navbar.jsx";
import PersonForm from "./components/person/PersonForm.jsx";
import PersonList from "./components/person/PersonList.jsx";
import EditPerson from "./components/person/EditPerson.jsx";


function App() {
  return (
    <BrowserRouter>
      <Navbar />
      <main className="p-8">Hello again</main>
      <PersonForm />
      <PersonList />
      <EditPerson />
    </BrowserRouter>
  );
}

export default App;
