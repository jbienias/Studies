class PlayersController < ApplicationController
  before_action :set_player, only: [:show, :edit, :update, :destroy]
  helper_method :sort_column, :sort_direction

  def index
    if params[:search]
      @players = Player.search(params[:search]).order("created_at DESC")
    else
      @players = Player.order("#{sort_column} #{sort_direction}")
    end
  end

  def show
  end

  def new
    @player = Player.new
    @teams = Team.all.order(:name)
  end

  def edit
    @teams = Team.all.order(:name)
  end

  def create
    @player = Player.new(player_params)
    @teams = Team.all.order(:name)

    if @player.save
      redirect_to @player, notice: 'Player was successfully created.'
    else
      render 'new'
    end
  end

  def update
    @teams = Team.all.order(:name)
    if @player.update(player_params)
      redirect_to @player, notice: 'Player was successfully updated.'
    else
      render 'edit'
    end
  end

  def destroy
    @player.destroy
    redirect_to players_path, notice: 'Player was successfully deleted.'
  end

  private

    def set_player
      @player = Player.find(params[:id])
    end

    def player_params
      params.require(:player).permit(:name, :surname, :nickname, :salary, :date_of_birth, :team_id)
    end

    def sortable_columns
      ["name", "surname", "nickname", "date_of_birth"]
    end

    def sort_column
      sortable_columns.include?(params[:column]) ? params[:column] : "name"
    end

    def sort_direction
      %w[asc desc].include?(params[:direction]) ? params[:direction] : "asc"
    end
end
