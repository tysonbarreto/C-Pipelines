import { useForm } from "react-hook-form";
import { Save, RotateCcw } from "lucide-react";
import { usePersonContext } from "../../context/PersonContext";

const PersonForm = () => {
  const {
    register,
    handleSubmit,
    reset,
    formState: { errors },
  } = useForm();

  const { addPerson } = usePersonContext();

  const onSubmit = (data) => {
    addPerson(data);
    reset();
  };

  return (
    <div
      className="p-4 border rounded-lg bg-white"
      style={{ marginBottom: "5px" }}
    >
      <form action="" className="space-y-4" onSubmit={handleSubmit(onSubmit)}>
        <div className="flex flex-col">
          <label className="text-sm font-medium text-gray-700 mb-1">
            First Name
          </label>
          <input
            type="text"
            placeholder="Enter first name"
            {...register("firstName", { required: "First name is required" })}
            className="border rounded-lg px-3 py-2 focus:outline-none focus:ring-2 focus:ring-blue-500"
          />
          {errors.firstName && (
            <p className="text-red-600 mt-1">{errors.firstName.message}</p>
          )}
        </div>
        <div className="flex flex-col">
          <label className="text-sm font-medium text-gray-700 mb-1">
            Last Name
          </label>
          <input
            type="text"
            placeholder="Enter last name"
            {...register("lastName", { required: "Last name is required" })}
            className="border rounded-lg px-3 py-2 focus:outline-none focus:ring-2 focus:ring-blue-500"
          />
          {errors.lastName && (
            <p className="text-red-600 mt-1">{errors.lastName.message}</p>
          )}
        </div>
        {/* Buttons */}
        <div className="flex gap-3 pt-2">
          <button
            type="submit"
            className="bg-blue-600 text-white px-4 py-2 rounded-lg hoover:bg-gray-300 transition"
          >
            <Save size={18} />
            Submit
          </button>
        </div>
        <button
          type="Submit"
          onClick={() => reset()}
          className="bg-gray-200 text-gray-700 px-4 py-2 rounded-lg hoover:bg-gray-300 transition"
        >
          <RotateCcw size={18} />
          Reset
        </button>
      </form>
    </div>
  );
};

export default PersonForm;
