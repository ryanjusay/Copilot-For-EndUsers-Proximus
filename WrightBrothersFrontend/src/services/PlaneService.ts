import axios from "axios";

class PlaneService {
  getPlanes() {
    return axios.get(API_URL);
  }

  getPlaneById(id: string) {
    const result = axios.get(`${API_URL}/planes/${id}`);
    console.log(result);
    return result;
  }

  createPlane(plane: any) {
    return axios.post(`${API_URL}/planes`, plane);
  }

  calculateFlightPath(plane: any) {
    return axios.post(`${API_URL}/planes/calculateFlightPath`, plane);
    
  }
}

export default new PlaneService();
