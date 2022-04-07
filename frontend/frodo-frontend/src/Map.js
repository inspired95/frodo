import 'leaflet/dist/leaflet.css';
import './App.css'
import React, { useState } from 'react'
import {
  MapContainer,
  Marker,
  Popup,
  TileLayer,
  useMapEvents,
} from 'react-leaflet'
import { render } from 'react-dom';
import L from 'leaflet';

delete L.Icon.Default.prototype._getIconUrl;

L.Icon.Default.mergeOptions({
    iconRetinaUrl: require('leaflet/dist/images/marker-icon-2x.png'),
    iconUrl: require('leaflet/dist/images/marker-icon.png'),
    shadowUrl: require('leaflet/dist/images/marker-shadow.png')
});

function LocationMarker(props) {

  const [position, setPosition] = useState(null)
  const map = useMapEvents({
    locationfound(e) {
      setPosition(e.latlng)
      map.flyTo(e.latlng, map.getZoom(), {
        animate: true,
        duration: 1.5
      })
    },
  })
  if (!props.locationDone){
    map.locate()
  }

  return position === null ? null : (
    <Marker position={position}>
      <Popup>You are here</Popup>
    </Marker>
  )
}

function FromLocationMarker(props) {

  const [position, setPosition] = useState(null)
  const map = useMapEvents({
      click(e) {
        console.log('fromLocationClick' + e.latlng)
        props.onClickInternal(e.latlng)
    },
  })

  return props.position === null ? null : (
    <Marker position={props.position}>
      <Popup>You are here</Popup>
    </Marker>
  )
}

class MyMapComponent extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      currentPos: null,
      markers: [],
      locationDone: false,
      startMarkerLocation: null,
      endMarkerLocation: null,
      currentMarkerIsStart: true
    };
  }

  handleClick(e){
    console.log('click ' + e)
    console.log('currentIsStart ' + this.state.currentMarkerIsStart )
    if (this.state.currentMarkerIsStart) {
      this.setState({startMarkerLocation: e})
    } else {
      this.setState({endMarkerLocation: e})
    }
    this.setState({currentMarkerIsStart: !this.state.currentMarkerIsStart})

    

  }

  handleLocationDone() {
    this.setState({locationDone: true})
  }

  render() {

    return (
      <MapContainer center={{ lat: 51.505, lng: -0.09 }} zoom={13} style={{ height: "100vh" }}   id="mapContainer">

        <LocationMarker onDone={() => this.handleLocationDone()} locationDone={this.state.locationDone}/>
        <FromLocationMarker 
          onClickInternal={this.state.currentMarkerIsStart ? (e) => this.handleClick(e) : () => console.log('start is ignored')} 
          position={this.state.startMarkerLocation}
          />
        <FromLocationMarker 
          onClickInternal={this.state.currentMarkerIsStart ? () => console.log('end is ignored') : (e) => this.handleClick(e)} 
          position={this.state.endMarkerLocation}
        />
        <TileLayer
          attribution='&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
          url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
        />
      </MapContainer>
    )
  }
}


function Map() {
  return (

    <MyMapComponent/>
  )
}

export default Map;
