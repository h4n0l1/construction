import axios from 'axios';

const symfonyApi = axios.create({
  baseURL: 'http://localhost:8081/api'
});

export const getSymfonyApi = () => {
  return symfonyApi.get('/constructions');
};
