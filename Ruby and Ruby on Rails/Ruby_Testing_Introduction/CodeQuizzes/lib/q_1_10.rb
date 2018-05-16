class String
  def parse_params
    if self.include? "?"
      params = self.split("?").last
      Hash[params.split("&").map {|f| f.split("=")}]
    else
      nil
    end
  end
end
