import axios from "axios";

class FlightService {
  calculateAerodynamics(planeId: string) {
    return axios.post(`${API_URL}/flights/${planeId}/calculateAerodynamics/`);
  }

  getFlightById(flightId: string) {
    return axios.get(`${API_URL}/flights/${flightId}`);
  }
}

export default new FlightService();
