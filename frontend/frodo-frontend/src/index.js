import React from 'react';
import ReactDOM from 'react-dom';
import './index.css';
import App from './App';
import Map from './components/Map';
import PlanTrip from './components/PlanTrip';
import CurrentTrip from './components/CurrentTrip';
import Navigation from "./components/Navigation";
import {BrowserRouter as Router, Route, Routes} from "react-router-dom";
import reportWebVitals from './reportWebVitals';

ReactDOM.render(
  <Router>
    <Navigation/>
    <Routes>
      <Route path="/" element={<Map/>} />
      <Route path="/planTrip" element={<PlanTrip/>} />
      <Route path="/currentTrip/:tripId" element={<CurrentTrip/>} />
    </Routes>
  </Router>,
  document.getElementById('root')
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
