import axios from 'axios';

const symfonyApi = axios.create({
  baseURL: 'http://localhost:8081/api'
});

export const getApiData = () => {
  return symfonyApi.get('/constructions');
};

export const getApiDataById = id => {
  return symfonyApi.get(`/constructions/${id}`);
};
