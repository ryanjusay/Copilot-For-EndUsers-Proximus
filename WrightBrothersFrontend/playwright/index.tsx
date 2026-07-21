import { beforeMount, afterMount } from '@playwright/experimental-ct-react/hooks';
import { BrowserRouter } from 'react-router-dom';

import '../src/index.css'

export type HooksConfig = {
  routing?: boolean;
}

beforeMount<HooksConfig>(async ({ hooksConfig, App }) => {
  console.log(`Before mount: ${JSON.stringify(hooksConfig)}`);

  if (hooksConfig?.routing)
     return <BrowserRouter><App /></BrowserRouter>;
});

afterMount<HooksConfig>(async () => {
  console.log(`After mount`);
});