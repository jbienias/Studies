require 'q_1_10'

describe "#parse_params" do
  it "converts url query parameters to a Hash" do
    url = "https://something.com?days_ago=1000&sort_by=value"
    expected = {"days_ago"=>"1000", "sort_by"=>"value"}
    expect(url.parse_params).to eq expected
  end

  it "returns nil if there are no query parameters" do
    url = "https://something.com"
    expect(url.parse_params).to be_nil
  end
end
