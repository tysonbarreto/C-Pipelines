import { usePersonContext } from "../../context/PersonContext";

const PersonList = () => {
  const { people, deletePerson, setEditingPerson } = usePersonContext();

  return (
    <div className="mt-6 p-4 bg-white rounded-xl shadow border">
      <h2 className="text-xl font-semibold mb-4">People</h2>
      {people.length === 0 ? (
        <p className="text-gray-500">No people found.</p>
      ) : (
        <ul className="space-y-2">
          {people.map((p) => (
            <li
              key={p.personId}
              className="border p-3 rounded-lg flex justify-between"
            >
              <span>
                {p.firstName} {p.lastName}
              </span>
              <button
                onClick={() => {
                  setEditingPerson(p);
                }}
                className="hover:text-green-800 hover:cursor-pointer ml-auto mr-2 px-3 shadow border rounded-xl"
              >
                Edit
              </button>
              <button
                onClick={() => {
                  deletePerson(p.personId);
                }}
                className="hover:text-red-800 hover:cursor-pointer px-3 shadow border rounded-xl"
              >
                Delete
              </button>
            </li>
          ))}
        </ul>
      )}
    </div>
  );
};

export default PersonList;
