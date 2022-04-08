import React, { useEffect, useState } from "react";
import axios from 'axios';
import  {useParams}  from "react-router-dom";

function CurrentTrip(props){
    let { tripId } = useParams();

    const [barcode, setBarcode] = useState(null);

    useEffect(async () => {
        const fetchData = async () => {
            const result = await axios.get('https://localhost:5001/api/Ticket/CurrentBarcode', { params: { journeyId: tripId } });
            const imageBlob = result.blob();
            const imageObjectURL = URL.createObjectURL(imageBlob);
            setBarcode(imageObjectURL);
        };
    
        fetchData();
      },[]);

    return (
        <div className="container">
          <img src={"https://localhost:5001/api/Ticket/CurrentBarcode?journeyId=" + tripId} alt="icons" />
          { tripId }
        </div>
      )
      
    }
    
    export default CurrentTrip;
