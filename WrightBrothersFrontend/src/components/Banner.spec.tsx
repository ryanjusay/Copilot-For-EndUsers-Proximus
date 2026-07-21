import { test, expect } from '@playwright/experimental-ct-react';
import Banner from './Banner';

test('should render banner with title', async ({ mount }) => { 
  const component = await mount(<Banner />);

  await expect(component).toContainText('Dawn of Aviation');
});