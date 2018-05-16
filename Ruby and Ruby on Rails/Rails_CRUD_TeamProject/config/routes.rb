Rails.application.routes.draw do
  resources :players
  resources :teams
  resources :matches
  resources :pages
  root 'pages#index'
end
