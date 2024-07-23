import axios from 'axios';

const dotnetApi = axios.create({
  baseURL: 'http://localhost:8080/api'
});

export const getDotnetApi = () => {
  return dotnetApi.get('/constructions');
};
