import React from "react";
import { useNavigate } from "react-router-dom";

type PlaneListProps = {
  planes?: { id: number; name: string; }[];
};

const PlaneList: React.FC<PlaneListProps> = (props: PlaneListProps) => {
  const navigate = useNavigate();

  const handleClick = (planeId: any, event: any) => {
    const liElement = event.currentTarget; // This ensures you get the li element
    const imgElement = liElement.querySelector("img"); // Find the img element within the li
    imgElement.classList.add("flying"); // Trigger the 'fly away' animation

    setTimeout(() => {
      navigate(`/planes/${planeId}`); // Delay navigation until the animation completes
    }, 2000); // Adjust timeout to match the duration of your flyAway animation
  };

  const planes = props.planes || [];


  return (
    <div>
      <ul className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-6">
        {planes.map((plane) => (
          <li
            key={plane.id}
            onClick={(e) => handleClick(plane.id, e)}
            className="hover-tilt cursor-pointer col-span-1 flex flex-col text-center bg-amber-100 rounded-lg shadow-lg divide-y divide-amber-200"
          >
            {/* Vintage card styling */}
            <div className="flex-1 flex flex-col p-8">
              <img
                className="w-32 flex-shrink-0 mx-auto rounded-full"
                src="./wright-brothers-plane.png"
                alt={plane.name}
                style={{ filter: "sepia(1)" }}
              />{" "}
              {/* Sepia filter for a vintage look */}
              <h3 className="mt-6 text-amber-900 text-lg leading-6 font-serif">
                {plane.name}
              </h3>{" "}
              {/* Vintage font and color */}
            </div>
          </li>
        ))}
      </ul>
    </div>
  );
};

export default PlaneList;
