import { useEffect } from "react";
import { useForm } from "react-hook-form";
import { usePersonContext } from "../../context/PersonContext";

const EditPersonModal = () => {
  const { editingPerson, setEditingPerson, updatePerson } = usePersonContext();

  const { register, handleSubmit, reset } = useForm();

  // Load selected person into form
  useEffect(() => {
    if (editingPerson) {
      reset(editingPerson);
    }
  }, [editingPerson, reset]);

  if (!editingPerson) return null; // modal closed

  const onSubmit = (data) => {
    updatePerson(data); // PUT API call
    setEditingPerson(null); // close modal
  };

  return (
    <div className="fixed inset-0 bg-black/50 flex items-center justify-center">
      <div className="bg-white p-6 rounded-xl shadow-xl w-96">
        <h2 className="text-xl font-semibold mb-4">Edit Person</h2>

        <form onSubmit={handleSubmit(onSubmit)} className="space-y-4">
          <div>
            <label className="text-sm font-medium">First Name</label>
            <input
              {...register("firstName")}
              className="border rounded-lg w-full px-3 py-2"
            />
          </div>

          <div>
            <label className="text-sm font-medium">Last Name</label>
            <input
              {...register("lastName")}
              className="border rounded-lg w-full px-3 py-2"
            />
          </div>

          <input type="hidden" {...register("personId")} />

          <div className="flex justify-end gap-3 pt-2">
            <button
              type="button"
              onClick={() => setEditingPerson(null)}
              className="px-4 py-2 bg-gray-200 rounded-lg"
            >
              Cancel
            </button>

            <button
              type="submit"
              className="px-4 py-2 bg-blue-600 text-white rounded-lg"
            >
              Update
            </button>
          </div>
        </form>
      </div>
    </div>
  );
};

export default EditPersonModal;
