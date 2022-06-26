import React from 'react';
import { v4 as uuidv4 } from 'uuid';

import Navigation from './Navigation';
import { menus } from './menus'

function MyNavigation(){

  const menusMarkup = menus.map(function(menu){
    return (
      <Navigation name={ menu.name } icon={ menu.icon } key={ uuidv4() }/>
    );
  });

  return (
    <div>
      { menusMarkup }
    </div>
  );
}

export default MyNavigation;