import axios from 'axios';

const dotnetApi = axios.create({
  baseURL: 'http://localhost:8080/api'
});

export const createApiData = data => {
  return dotnetApi.post('/constructions', data);
};

export const updateApiData = (id, data) => {
  return dotnetApi.put(`/constructions/${id}`, data);
};

export const loginApiData = (data) => {
  return dotnetApi.post(`/users/login`, data);
};

