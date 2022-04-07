import React from 'react';
import ReactDOM from 'react-dom';
import './index.css';
import App from './App';
import PlanTrip from './components/PlanTrip';
import Navigation from "./components/Navigation";
import {BrowserRouter as Router, Route, Routes} from "react-router-dom";
import reportWebVitals from './reportWebVitals';

ReactDOM.render(
  <Router>
    <Navigation/>
    <Routes>
      <Route path="/" element={<App/>} />
      <Route path="/planTrip" element={<PlanTrip/>} />
    </Routes>
  </Router>,
  document.getElementById('root')
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
