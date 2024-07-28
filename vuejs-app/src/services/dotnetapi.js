import axios from 'axios';

const dotnetApi = axios.create({
  baseURL: 'https://dotnet.dennytriana.com/api'
});

export const createApiData = data => {
  return dotnetApi.post('/constructions', data);
};

export const updateApiData = (id, data) => {
  return dotnetApi.put(`/constructions/${id}`, data);
};

export const deleteApiData = (id) => {
  return dotnetApi.delete(`/constructions/${id}`);
};

export const loginApiData = (data) => {
  return dotnetApi.post(`/users/login`, data);
};

