//create provider component
import { useEffect, useState } from "react";
import { PersonContext } from "./PersonContext.jsx";

export const PersonProvider = ({ children }) => {
  const [people, setPeople] = useState([]);
  const [editingPerson, setEditingPerson] = useState(null);
  const API_URL = "http://localhost:3000/api/people";

  //GET ALL
  const fetchPeople = async () => {
    const res = await fetch(API_URL);
    const data = await res.json();
    console.log(data);

    setPeople(data);
  };

  //CREATE
  const addPerson = async (person) => {
    const res = await fetch(API_URL, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(person),
    });
    const newPerson = await res.json();
    setPeople((prev) => [...prev, newPerson]);
  };

  //DELETE
  const deletePerson = async (id) => {
    await fetch(`${API_URL}/${id}`, {
      method: "DELETE",
    });
    setPeople((prev) => prev.filter((person) => person.personId !== id));
  };

  //UPDATE
  const updatePerson = async (person) => {
    const res = await fetch(`${API_URL}/${person.personId}`, {
      method: "PUT",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(person),
    });
    const updated = await res.jsonn();
    setPeople((prev) =>
      prev.map((p) => (p.personId === updated.personId ? updated : p)),
    );
  };

  //load people on mount
  useEffect(() => {
    async function fetchAllPeople() {
      return await fetchPeople();
    }
    fetchAllPeople();
  }, []);

  const value = {
    people,
    fetchPeople,
    addPerson,
    deletePerson,
    updatePerson,

    editingPerson,
    setEditingPerson
  };

  return (
    <PersonContext.Provider value={value}>{children}</PersonContext.Provider>
  );
};
