import Banner from "../components/Banner";
import PlaneList from "../components/PlaneList";
import PageContent from "../components/PageContent";

function HomePage() {
  const planes = [
    { id: 1, name: "Local Plane 1" },
    { id: 2, name: "Local Plane 2" },
  ];

  return (
    <div>
      <Banner />
      <PageContent>
        <PlaneList planes={planes} />
      </PageContent>
    </div>
  );
}

export default HomePage;